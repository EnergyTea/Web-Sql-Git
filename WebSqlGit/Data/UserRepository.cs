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
    public class UserRepository : IUserInterface
    {
        readonly string connectionString;
        public UserRepository(string conn)
        {
            connectionString = conn;
        }

        public User Authorize(User user)
        {
            using (IDbConnection db = new SqlConnection(connectionString)) { 
                string Login = user.Login;
                string Password = user.Password;
                User AuthorizeUser = db.Query<User>("SELECT * FROM Users WHERE Login = @login AND Password = @Password", new{ Login, Password }).FirstOrDefault();
                return AuthorizeUser;
            }
        }

        public List<User> GetUsers()
        {
            using (IDbConnection db = new SqlConnection(connectionString)) { 
                List<User> users = db.Query<User>("SELECT Login FROM Users").ToList();
                return users.ToList();
            }
        }

        public string GetUser(string UserName)
        {
            using (IDbConnection db = new SqlConnection(connectionString)) {
                string AuthorizeUser = db.Query<string>("SELECT Name FROM Users WHERE Login = @UserName", new { UserName }).FirstOrDefault();
                return AuthorizeUser; 
            }
        }

        public User RegistrationUser(User user)
        {
            using (IDbConnection db = new SqlConnection(connectionString)) { 
                    if (!db.ExecuteScalar<bool>("SELECT * FROM Users WHERE Login = @Login", user))
                    {
                        User NewUser = db.Query<User>("INSERT INTO Users (Name, Login, Password) VALUES(@Name, @Login, @Password)", user).FirstOrDefault();
                        return NewUser;
                    }
                return null;
            }
        }
    }
}
