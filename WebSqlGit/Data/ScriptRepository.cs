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

        public List<Script> GetAll()
        {
            using IDbConnection db = new SqlConnection(connectionString);
            List<Script> scripts = db.Query<Script>("SELECT Scripts.Id, Scripts.CategoryId, ScriptsHistory.Name FROM Scripts, ScriptsHistory").ToList(); // заджойнить Name из ScriptHistory
                                                                                                                    //"LEFT JOIN ( SELECT ScriptsHistory.Name FROM ScriptsHistory"
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
            return db.Query<Script>("SELECT * FROM ScriptsHistory WHERE ScriptId = @id", new { id }).FirstOrDefault(); // Добавить проверку на bool IsLastVersion
        }

        public void CreateScript(Script script)
        {
            using IDbConnection db = new SqlConnection(connectionString);
            script.ScriptId = 7;  //"SELECT MAX(Id) FROM Scripts"
            script.Version = 1; // Сделать 1.00
            script.CreationDataTime = DateTime.Now;
            script.UpdateDataTime = DateTime.Now; // Должно быть 2 запроса. Для создания Script`a и создания ScriptsHistory
            //var sqlQuery1 = "INSERT INTO Scripts (CategoryId, CreationDataTime) VALUES(@CategoryId, @CreationDataTime)";
                
            var sqlQuery = "INSERT INTO ScriptsHistory (ScriptId, CategoryId, Version, Name, Body, Author, CreationDataTime, UpdateDataTime, IsLastVersion) " +
                "VALUES(@ScriptId, @CategoryId, @Version, @Name, @Body, @Author, @CreationDataTime, @UpdateDataTime, @IsLastVersion) " +
                "INSERT INTO Scripts (CategoryId, CreationDataTime)" +
                "VALUES(@CategoryId,  @CreationDataTime )";
            db.Execute(sqlQuery, script);
        }

        public void DeleteScript(int id)
        {
            using IDbConnection db = new SqlConnection(connectionString);
            var sqlQuery = "UPDATE Scripts SET Deleted = 1 WHERE Id = @id";
            db.Execute(sqlQuery, new { id });
        }

        public void UpdateScript(Script script) // При создании скрипта, у нас должно что-то увеличиваться. А что-то остается const
        {
            using IDbConnection db = new SqlConnection(connectionString);
            var sqlQuery = "INSERT INTO ScriptsHistory (ScriptId, CategoryId, Version, Name, Body, Author, CreationDataTime, UpdateDataTime, IsLastVersion) VALUES(@ScriptId, @CategoryId, @Version, @Name, @Body, @Author, @CreationDataTime, @UpdateDataTime, @IsLastVersion)";
            db.Execute(sqlQuery, script);
        }

        public List<Script> GetScriptsForCategory(int id)
        {
            using IDbConnection db = new SqlConnection(connectionString);
            return db.Query<Script>("SELECT Scripts.Id, Scripts.CategoryId, ScriptsHistory.Name FROM Scripts, ScriptsHistory WHERE Scripts.CategoryId = @id", new { id }).ToList(); // Добавить проверку на bool IsLastVersion
        }
    }
}
