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
            List<Script> scripts = db.Query<Script>("SELECT * FROM Scripts").ToList();
            var scripts1 = scripts.Select(s => new Script
            {
                Id = s.Id,
                CategoryId = s.CategoryId,
                CreationDataTime = s.CreationDataTime,
                // нужно выводить Name из ScriptHistory
            });
            return scripts1.ToList();
        }

        public Script GetScript(int id)
        {
            using IDbConnection db = new SqlConnection(connectionString);
            return db.Query<Script>("SELECT * FROM ScriptsHistory WHERE Id = @id", new { id }).FirstOrDefault(); // Добавить проверку на bool IsLastVersion
        }

        public void CreateScript(Script script)
        {
            using IDbConnection db = new SqlConnection(connectionString);
            var sqlQuery = "INSERT INTO ScriptsHistory (ScriptId, CategoryId, Version, Name, Body, Author, CreationDataTime, UpdateDataTime, IsLastVersion) VALUES(@ScriptId, @CategoryId, @Version, @Name, @Body, @Author, @CreationDataTime, @UpdateDataTime, @IsLastVersion)";
            db.Execute(sqlQuery, script);
        }

        public void DeleteScript(int id)
        {
            using IDbConnection db = new SqlConnection(connectionString);
            var sqlQuery = "DELETE FROM Scripts WHERE Id = @id";
            db.Execute(sqlQuery, new { id });
        }

        public void UpdateScript(Script script) // При создании скрипта, у нас должно что-то увеличиваться. 
        {
            using IDbConnection db = new SqlConnection(connectionString);
            var sqlQuery = "INSERT INTO ScriptsHistory (ScriptId, CategoryId, Version, Name, Body, Author, CreationDataTime, UpdateDataTime, IsLastVersion) VALUES(@ScriptId, @CategoryId, @Version, @Name, @Body, @Author, @CreationDataTime, @UpdateDataTime, @IsLastVersion)";
            db.Execute(sqlQuery, script);
        }

        public List<Script> GetScriptsForCategory(int categoryId)
        {
            using IDbConnection db = new SqlConnection(connectionString);
            return db.Query<Script>("SELECT * FROM ScriptsHistory WHERE Id = @id", new { categoryId }).ToList(); // Добавить проверку на bool IsLastVersion
        }
    }
}
