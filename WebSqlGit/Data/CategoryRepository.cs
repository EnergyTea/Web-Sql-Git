using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSqlGit.Data.Interface;
using WebSqlGit.Data.Model;

namespace WebSqlGit.Data
{
    public class CategoryRepository : ICategoryInterface
    {
         private List<Category> Categories = new List<Category>()
            {
               new Category { 
                    Id = 1, 
                    Name = "HomeProject",
                    ScriptId = new int[2] { 1 , 2 } },
               new Category {
                    Id = 2,
                    Name = "Gleb Jobee",
                    ScriptId =  new int[3] { 1, 2, 3} },
               new Category { 
                    Id = 3, 
                    Name = "Hotel P",
                    ScriptId = new int[3] {1, 2, 3 } },
               new Category { 
                    Id = 4, 
                    Name = "Work",
                    ScriptId = new int[1] { 2 } },
            };

        public Category CreateCategory(string name = null)
        {
            var category = new Category { Id = Categories.Count > 0 ? Categories.Max(c => c.Id) + 1 : 1, Name = name};
            Categories.Add(category);
            return new Category
            {
                Id = category.Id,
                Name = category.Name,
                ScriptId = category.ScriptId
            };
        }

        public Category DeleteCategory(int id)
        {
            var category = Categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return null;
            }
            Categories = Categories.Where(c => c.Id != id).ToList();
            return new Category
            {
                Id = category.Id,
                Name = category.Name,
                ScriptId = category.ScriptId
            };
        }

        public IEnumerable<Category> GetAll()
        {
            var categories = Categories.Select(c => new Category
            {
                Id = c.Id,
                Name = c.Name,
                ScriptId = c.ScriptId
            });
            return categories.ToList();
        }
    }
}
