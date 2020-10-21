using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using WebSqlGit.Data.Interface;
using WebSqlGit.Data.Model;
using WebSqlGit.Helpes;

namespace WebSqlGit.Data
{
    public class UserRepository : IUserRepository
    {
        readonly string _connectionString;
        
        public UserRepository( string connectionString )
        {
            _connectionString = connectionString;
        }

        public User Authorize( User user )
        {
            user.Password = HashHelper.TextToHash( user.Password );
            using ( IDbConnection db = new SqlConnection( _connectionString ) ) 
            { 
                string sqlQuery = "SELECT * FROM Users WHERE Login = @login AND Password = @Password";
                User AuthorizeUser = db.Query<User>( sqlQuery, new{ user.Login, user.Password } ).FirstOrDefault();
                
                return AuthorizeUser;
            }
        }

        public string GetUserNameByLogin( string login )
        {
            using ( IDbConnection db = new SqlConnection( _connectionString ) ) 
            {
                string sqlQuery = "SELECT Name FROM Users WHERE Login = @login";
                
                return db.Query<string>( sqlQuery, new { login } ).FirstOrDefault(); 
            }
        }

        public void RegistrationUser( User user )
        {
            using ( IDbConnection db = new SqlConnection( _connectionString ) )
            {
                user.Password = HashHelper.TextToHash( user.Password );
                if ( !db.ExecuteScalar<bool>( "SELECT * FROM Users WHERE Login = @Login", user ) )
                {
                    string sqlQuery = "INSERT INTO Users (Name, Login, Password) VALUES(@Name, @Login, @Password)";
                    db.Query<User>( sqlQuery, user ).FirstOrDefault();
                }
            }
        }
    }
}
