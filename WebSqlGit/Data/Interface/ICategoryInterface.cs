using System.Collections.Generic;
using WebSqlGit.Data.Model;

namespace WebSqlGit.Data.Interface
{
    public interface ICategoryInterface
    {
        IEnumerable<Category> GetAll();
        Category GetCategory(int id);
        void CreateCategory(Category category);
    }
}
