using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using WebSqlGit.Data.Interface;
using WebSqlGit.Data.Model;

namespace WebSqlGit.Data
{
    public class UserRepository : IUserRepository
    {
        readonly string connectionString;
        
        public UserRepository( string connection )
        {
            connectionString = connection;
        }

        public User Authorize( User user )
        {
            using ( IDbConnection db = new SqlConnection( connectionString ) ) 
            { 
                string sqlQuery = "SELECT * FROM Users WHERE Login = @login AND Password = @Password";
                User AuthorizeUser = db.Query<User>( sqlQuery, new{ user.Login, user.Password } ).FirstOrDefault();
                
                return AuthorizeUser;
            }
        }

        public List<User> GetUsers()
        {
            using ( IDbConnection db = new SqlConnection( connectionString ) ) 
            { 
                return db.Query<User>( "SELECT Login FROM Users" ).ToList();
            }
        }

        public string GetUserNameByLogin( string login )
        {
            using ( IDbConnection db = new SqlConnection( connectionString ) ) 
            {
                string sqlQuery = "SELECT Name FROM Users WHERE Login = @UserName";
                
                return db.Query<string>( sqlQuery, new { login } ).FirstOrDefault(); 
            }
        }

        public void RegistrationUser( User user )
        {
            using ( IDbConnection db = new SqlConnection( connectionString ) )
            {
                if ( !db.ExecuteScalar<bool>( "SELECT * FROM Users WHERE Login = @Login", user ) )
                {
                    string sqlQuery = "INSERT INTO Users (Name, Login, Password) VALUES(@Name, @Login, @Password)";
                    db.Query<User>( sqlQuery, user ).FirstOrDefault();
                }
            }
        }
    }
}
