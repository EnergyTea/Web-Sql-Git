using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebSqlGit.Data.Interface;
using WebSqlGit.Data.Model;
using WebSqlGit.Model;

namespace WebSqlGit.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryInterface _categoryInterface;

        public CategoryController( ICategoryInterface categoryInterface )
        {
            _categoryInterface = categoryInterface;
        }

        [HttpGet]
        public List<Category> CategoryList()
        {
            var category = _categoryInterface.GetAll();
            return category.ToList();
        }
        [HttpDelete]
        public IActionResult DeleteCategory(int id)
        {
            Category category = _categoryInterface.DeleteCategory(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }
        [HttpPost]
        public IActionResult CreateCategory([FromForm] Category category)
        {
            Category created = _categoryInterface.CreateCategory(category.Name);
            return Ok(created);
        }
    }
}