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
            using IDbConnection db = new SqlConnection(connectionString);
            string Login = user.Login;
            string Password = user.Password;
            User AuthorizeUser = db.Query<User>("SELECT * FROM Users WHERE Login = @login AND Password = @Password", new{ Login, Password }).FirstOrDefault();
            return AuthorizeUser;
        }
    }
}
