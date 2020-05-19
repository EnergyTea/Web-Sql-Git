using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebSqlGit.Data.Interface;
using WebSqlGit.Data.Model;

namespace WebSqlGit.Data
{
    public class CategoryRepository : ICategoryInterface
    {
        readonly string connectionString;
        public CategoryRepository(string conn)
        {
            connectionString = conn;
        }

        public void CreateCategory(Category category)
        {
            using IDbConnection db = new SqlConnection(connectionString);
            var sqlQuery = "INSERT INTO Users (Name, Login, Password) VALUES(@Name, @Login, @Password)";
            db.Execute(sqlQuery, category);
        }

        public void DeleteCategory(int id)
        {
            using IDbConnection db = new SqlConnection(connectionString);
            var sqlQuery = "UPDATE Categories SET Deleted = 1 WHERE (Id = @id)";
            db.Execute(sqlQuery, new { id });
        }

        public IEnumerable<Category> GetAll()
        {
            using IDbConnection db = new SqlConnection(connectionString);
            return db.Query<Category>("SELECT * FROM Categories WHERE Deleted IS NULL");
        }

        public Category GetCategory(int id)
        {
            using IDbConnection db = new SqlConnection(connectionString);
            return db.Query<Category>("SELECT * FROM Categories WHERE Id = @id AND Deleted IS NULL", new { id }).FirstOrDefault();
        }
    }
}
