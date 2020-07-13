using System.Collections.Generic;
using WebSqlGit.Data.Model;

namespace WebSqlGit.Data.Interface
{
    public interface IUserInterface
    {
       List<User> GetUsers();
       User Authorize(User user);
       User RegistrationUser(User user);
       string GetUser(string userName);
    }
}
