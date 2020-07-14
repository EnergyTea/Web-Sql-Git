using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using WebSqlGit.Data.Interface;
using WebSqlGit.Data.Model;
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
            using (IDbConnection db = new SqlConnection(connectionString)) { 
                List<Script> scripts = db.Query<Script>(
                   "SELECT Scripts.Id, Scripts.CategoryId, ScriptsHistory.Name FROM ScriptsHistory " +
                   "JOIN Scripts ON Scripts.Id = ScriptsHistory.ScriptId WHERE ScriptsHistory.Deleted IS NULL AND ScriptsHistory.IsLastVersion = 1"
                    ).ToList();
                List<Script> scriptsPush = scripts.Select(s => new Script
                {
                    Id = s.Id,
                    Name = s.Name,
                    CategoryId = s.CategoryId,
                    CreationDataTime = s.CreationDataTime,
                }).ToList();
                return scriptsPush;
            }
        }

        public Script GetScript(int id, string author) {
            using (IDbConnection db = new SqlConnection(connectionString)) {
                Script script = db.Query<Script>(
                    "SELECT * FROM ScriptsHistory WHERE ScriptId = @id AND IsLastVersion = 1 AND ScriptsHistory.Deleted IS NULL",
                new { id }
                ).FirstOrDefault();
                if (script != null) 
                { 
                    int scriptId = script.Id;
                    script.IsAuthor = CheckAuthoriz(id, author);
                    script.Tags = db.Query<String>("SELECT Name FROM Tags WHERE ScriptsHistoryId = @scriptId", new { scriptId }).ToArray();
                }
                return script;
            }
        }

        public void CreateScript(Script script)
        {
            using (IDbConnection db = new SqlConnection(connectionString)) { 
                script.Version = 1;
                script.CreationDataTime = DateTime.Now;
                script.IsLastVersion = true;
                script.UpdateDataTime = DateTime.Now;
                script.AuthorId = db.Query<int>("SELECT Id FROM Users WHERE Login = @Author", script).LastOrDefault();
                script.Author = db.Query<string>("SELECT Name FROM Users WHERE Id = @AuthorId", script).LastOrDefault();
                string SqlScript =
                    @"INSERT INTO  Scripts (CategoryId, CreationDataTime, AuthorId) VALUES(@CategoryId,  @CreationDataTime, @AuthorId ); 
                      SELECT CAST(SCOPE_IDENTITY() as int)";
                int Id = db.Query<int>(SqlScript, script).LastOrDefault();
                script.ScriptId = Id;
                string sqlQuery =
                    "INSERT INTO ScriptsHistory (ScriptId, CategoryId, Version, Name, Body, Author, AuthorId, CreationDataTime, UpdateDataTime, IsLastVersion) " +
                    "VALUES(@ScriptId, @CategoryId, @Version, @Name, @Body, @Author, @AuthorId, @CreationDataTime, @UpdateDataTime, @IsLastVersion);" +
                    "SELECT CAST(SCOPE_IDENTITY() as int) ";
                int ScriptsHistoryId  = db.Query<int>(sqlQuery, script).LastOrDefault();
                // add tag in db.tags
                var parameters = script.Tags.Select(u =>
                {
                    var tempParams = new DynamicParameters();
                    tempParams.Add("@tag", u, DbType.String, ParameterDirection.Input);
                    return tempParams;
                });            
                string sqlTags = "INSERT INTO Tags (Name, ScriptsHistoryId) VALUES(@tag, "+ScriptsHistoryId+" );";
                db.Execute(sqlTags, parameters);
            }            
        }

        public void DeleteScript(int id, string author)
        {
            using (IDbConnection db = new SqlConnection(connectionString)) { 
                if (CheckAuthoriz(id, author))
                {
                    string sqlQuery =
                    "UPDATE Scripts SET Deleted = 1 WHERE (Id = @id); " +
                    "UPDATE ScriptsHistory SET Deleted = 1 WHERE (ScriptId = @id);";
                    db.Execute(sqlQuery, new { id });
                }
            }                        
        }

        public void UpdateScript(Script script, string author) 
        {
            using (IDbConnection db = new SqlConnection(connectionString)) {             
                if (CheckAuthoriz(script.ScriptId, author))
                {
                    int id = script.ScriptId;
                    string sqlScript =
                        "SELECT * FROM ScriptsHistory WHERE ScriptId = @id AND IsLastVersion = 'true'";
                    Script scriptUpdate = db.Query<Script>(sqlScript, new { id } ).First();
                    scriptUpdate.Version += 1;
                    scriptUpdate.UpdateDataTime = DateTime.Now;
                    scriptUpdate.Name = script.Name;
                    scriptUpdate.Id = 0;
                    scriptUpdate.Body = script.Body;
                    string sqlLastVersion = 
                        "UPDATE ScriptsHistory SET IsLastVersion = 'false' WHERE ScriptId = @id";
                    db.Execute(sqlLastVersion, new { id });
                    scriptUpdate.IsLastVersion = true;
                    string sqlQuery =
                        "INSERT INTO ScriptsHistory (ScriptId, CreationDataTime, CategoryId, Version, Name, Body, Author, AuthorId, UpdateDataTime, IsLastVersion) " +
                        "VALUES(@ScriptId, @CreationDataTime, @CategoryId, @Version, @Name, @Body, @Author, @AuthorId, @UpdateDataTime, @IsLastVersion);" +
                        "SELECT CAST(SCOPE_IDENTITY() as int) ";
                    int HistoryId = db.Query<int>(sqlQuery, scriptUpdate).First();
                    var parameters = script.Tags.Select(u =>
                    {
                        var tempParams = new DynamicParameters();
                        tempParams.Add("@tag", u, DbType.String, ParameterDirection.Input);
                        return tempParams;
                    });
                    string sqlTags = "INSERT INTO Tags (Name, ScriptsHistoryId) VALUES(@tag, " + HistoryId + " );";
                    db.Execute(sqlTags, parameters);
                }
            }
        }

        public List<Script> GetScriptsForCategory(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString)) { 
                return db.Query<Script>(
                    "SELECT Scripts.Id, Scripts.CategoryId, ScriptsHistory.Name FROM ScriptsHistory " +
                    "LEFT JOIN Scripts ON Scripts.Id = ScriptsHistory.ScriptId WHERE ScriptsHistory.Deleted IS NULL AND ScriptsHistory.IsLastVersion = 1 AND Scripts.CategoryId = @id",
                new { id }
                ).ToList();
            }
        }

        public List<Script> GetScriptsHistory(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString)) { 
                return db.Query<Script>(
                    "SELECT * FROM ScriptsHistory WHERE ScriptId = @id AND ScriptsHistory.Deleted IS NULL",
                new { id }
                ).ToList();
            }
        }

        public Script GetScriptHistory(int id, string author)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                Script script = db.Query<Script>(
                    "SELECT * FROM ScriptsHistory WHERE Id = @id AND ScriptsHistory.Deleted IS NULL",
                new { id }
                ).FirstOrDefault();
                script.IsAuthor = CheckAuthoriz(script.ScriptId, author);
                script.Tags = db.Query<String>("SELECT Name FROM Tags WHERE ScriptsHistoryId = @id", new { id }).ToArray();
                return script;
            }
        }

        public List<Script> GetUserScripts(string author)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                int id = db.Query<int>("SELECT Id FROM Users WHERE Login = @author", new { author }).First();
                List<Script> scripts = db.Query<Script>(
                   "SELECT Scripts.Id, Scripts.CategoryId, ScriptsHistory.Name FROM ScriptsHistory " +
                   "JOIN Scripts ON Scripts.Id = ScriptsHistory.ScriptId WHERE Scripts.AuthorId = @id AND ScriptsHistory.Deleted IS NULL AND ScriptsHistory.IsLastVersion = 1", new { id }
                    ).ToList();
                List<Script> scriptsPush = scripts.Select(s => new Script
                {
                    Id = s.Id,
                    Name = s.Name,
                    CategoryId = s.CategoryId,
                    CreationDataTime = s.CreationDataTime,
                }).ToList();
                return scriptsPush;
            }
        }

        public List<Script> GetScriptsBySearchPattern(string pattern)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                List<Script> scriptsByName = SearchScriptsByNameEntry(pattern);
                List<Script> scriptsByTag = SearchScriptsByTagEntry(pattern);
                scriptsByName.Union(scriptsByTag).Take(10).ToList();
                List<Script> scriptsPush = scriptsByName.Select(script => new Script
                {
                    Id = script.ScriptId,
                    Name = script.Name,
                    AuthorId = script.AuthorId
                }).ToList();
                return scriptsPush;
            }
        }

        public List<Script> SearchScriptsByNameEntry(string pattern)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                List<Script> scriptsByName = db.Query<Script>("SELECT * FROM ScriptsHistory WHERE IsLastVersion = 'True' AND Deleted IS NULL AND Name LIKE  @Pattern", new { Pattern = "%" + pattern + "%" }).ToList();
                return scriptsByName;
            }
        }

        public List<Script> SearchScriptsByTagEntry(string pattern)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                List<int> scriptsByTag = db.Query<int>("SELECT ScriptsHistoryId FROM Tags WHERE Name LIKE  @Pattern", new { Pattern = "%" + pattern + "%" }).ToList();
                string sqlTags = "SELECT * FROM ScriptsHistory WHERE IsLastVersion = 'True' AND Deleted IS NULL AND ScriptId IN @scriptsByTag";
                List<Script> scriptInTags = db.Query<Script>(sqlTags, new { scriptsByTag }).ToList();
                return scriptInTags;
            }
        }

        public bool CheckAuthoriz(int id, string author)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (author == null) 
                { 
                    return false;
                };
                int UserId = db.Query<int>("SELECT Id FROM Users WHERE Login = @Author", new { author }).First();
                int AuthorId = db.Query<int>("SELECT AuthorId FROM Scripts WHERE Id = @id", new { id }).First();
                return UserId == AuthorId;
            }
        }        
    }
}
