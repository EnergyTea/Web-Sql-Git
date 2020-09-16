using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using WebSqlGit.Data.Interface;
using WebSqlGit.Data.Model;

namespace WebSqlGit.Data
{
    public class CategoryRepository : ICategoryRepository
    {
        readonly string connectionString;
        
        public CategoryRepository( string connection )
        {
            connectionString = connection;
        }

        public void CreateCategory( Category category )
        {
            using ( IDbConnection db = new SqlConnection( connectionString ) )
            { 
                string sqlQuery = "INSERT INTO Categories (Name) VALUES(@Name)";
                db.Execute( sqlQuery, category );
            }
        }

        public List<Category> GetAll()
        {
            using ( IDbConnection db = new SqlConnection( connectionString ) )
            {
                string sqlQuery = "SELECT * FROM Categories WHERE Deleted IS NULL";

                return db.Query<Category>( sqlQuery ).ToList();
            }
        }

        public Category GetCategory( int id )
        {
            using ( IDbConnection db = new SqlConnection( connectionString ) ) 
            {
                string sqlQuery = "SELECT * FROM Categories WHERE Id = @id AND Deleted IS NULL";
                
                return db.Query<Category>( sqlQuery, new { id } ).FirstOrDefault();
            }
        }
    }
}
