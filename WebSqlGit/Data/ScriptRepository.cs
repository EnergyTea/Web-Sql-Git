using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using WebSqlGit.Data.Interface;
using WebSqlGit.Model;

namespace WebSqlGit.Data
{
    public class ScriptRepository : IScriptInterface
    {
        readonly string connectionString;
        public ScriptRepository(string conn)
        {
            connectionString = conn;
        }

        // Выводим все скрипты в Script List
        public List<Script> GetAll()
        {
            using IDbConnection db = new SqlConnection(connectionString);
            List<Script> scripts = db.Query<Script>("SELECT Scripts.Id, Scripts.CategoryId, ScriptsHistory.Name FROM ScriptsHistory LEFT JOIN Scripts ON Scripts.Id = ScriptsHistory.ScriptId WHERE ScriptsHistory.Deleted IS NULL AND ScriptsHistory.IsLastVersion = 1").ToList(); // заджойнить Name из ScriptHistory
                                                                                                                    //"LEFT JOIN ( SELECT ScriptsHistory.Name FROM ScriptsHistory ON id = scritid"
            var scripts1 = scripts.Select(s => new Script
            {
                Id = s.Id,
                Name = s.Name,
                CategoryId = s.CategoryId,
                CreationDataTime = s.CreationDataTime,
            });
            return scripts1.ToList();
        }

        public Script GetScript(int id)
        {
            using IDbConnection db = new SqlConnection(connectionString);
            return db.Query<Script>("SELECT * FROM ScriptsHistory WHERE ScriptId = @id AND IsLastVersion = 1 AND ScriptsHistory.Deleted IS NULL", new { id }).FirstOrDefault(); // Добавить проверку на bool IsLastVersion
        }

        public void CreateScript(Script script)
        {
            using IDbConnection db = new SqlConnection(connectionString);
            script.Version = 1;
            script.CreationDataTime = DateTime.Now;
            script.IsLastVersion = true;
            script.UpdateDataTime = DateTime.Now; 
            var SqlScript =
                @"INSERT INTO  Scripts (CategoryId, CreationDataTime) VALUES(@CategoryId,  @CreationDataTime ); 
                  SELECT CAST(SCOPE_IDENTITY() as int)";
            var Id = db.Query<int>(SqlScript, script).LastOrDefault();
            script.ScriptId = Id;
            var sqlQuery = 
                "INSERT INTO ScriptsHistory (ScriptId, CategoryId, Version, Name, Body, Author, CreationDataTime, UpdateDataTime, IsLastVersion) " +
                "VALUES(@ScriptId, @CategoryId, @Version, @Name, @Body, @Author, @CreationDataTime, @UpdateDataTime, @IsLastVersion) ";
            db.Execute(sqlQuery, script);
        }

        public void DeleteScript(int id)
        {
            using IDbConnection db = new SqlConnection(connectionString);
            var sqlQuery = "UPDATE Scripts SET Deleted = 1 WHERE (Id = @id)";
            db.Execute(sqlQuery, new { id });
        }

        public void UpdateScript(Script script) // При создании скрипта, у нас должно что-то увеличиваться. А что-то остается const
        {
            using IDbConnection db = new SqlConnection(connectionString);
            int id = script.ScriptId;
            var sqlScript = "SELECT * FROM ScriptsHistory WHERE ScriptId = @id";
            Script script2 = db.Query<Script>(sqlScript, new { id } ).First();
            script2.Version += 1;
            script2.UpdateDataTime = DateTime.Now;
            script2.Name = script.Name;
            script2.Id = 0;
            script2.Author = script.Author;
            script2.Body = script.Body;
            var sqlLastVersion = "UPDATE ScriptsHistory SET IsLastVersion = 'false' WHERE ScriptId = @id";
            db.Execute(sqlLastVersion, new { id });
            script2.IsLastVersion = true;
            var sqlQuery = "INSERT INTO ScriptsHistory (ScriptId, CreationDataTime, CategoryId, Version, Name, Body, Author, UpdateDataTime, IsLastVersion) VALUES(@ScriptId, @CreationDataTime, @CategoryId, @Version, @Name, @Body, @Author, @UpdateDataTime, @IsLastVersion)";
            db.Execute(sqlQuery, script2);
        }

        public List<Script> GetScriptsForCategory(int id)
        {
            using IDbConnection db = new SqlConnection(connectionString);
            return db.Query<Script>("SELECT Scripts.Id, Scripts.CategoryId, ScriptsHistory.Name FROM ScriptsHistory LEFT JOIN Scripts ON Scripts.Id = ScriptsHistory.ScriptId WHERE Scripts.CategoryId = @id", new { id }).ToList(); // Добавить проверку на bool IsLastVersion
        }
    }
}
