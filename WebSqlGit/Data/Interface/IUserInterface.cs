using System.Threading.Tasks;
using WebSqlGit.Data.Model;

namespace WebSqlGit.Data.Interface
{
    public interface IUserInterface
    {
       User Authorize(User user);
    }
}
