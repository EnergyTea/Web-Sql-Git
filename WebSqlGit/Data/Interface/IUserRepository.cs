using System.Collections.Generic;
using WebSqlGit.Data.Model;

namespace WebSqlGit.Data.Interface
{
    public interface IUserRepository
    {
       List<User> GetUsers();
       User Authorize( User user );
       void RegistrationUser( User user );
       string GetUserNameByLogin( string name );
    }
}
