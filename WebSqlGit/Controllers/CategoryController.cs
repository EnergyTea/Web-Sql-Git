using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IScriptInterface _scriptInterface;

        public CategoryController(ICategoryInterface categoryInterface, IScriptInterface scriptInterface)
        {
            _categoryInterface = categoryInterface;
            _scriptInterface = scriptInterface;
        }

        [HttpGet]
        public List<Category> CategoryList()
        {
            var category = _categoryInterface.GetAll();
            return category.ToList();
        }
        [HttpGet("{id}")]
        public IActionResult GetCategory(int id)
        {
            Category category = _categoryInterface.GetCategory(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }
        [HttpPost("{id}/delete")]
        [Authorize]
        public IActionResult DeleteCategory(int id)
        {
            _categoryInterface.DeleteCategory(id);
            return NotFound();
        }
        [HttpPost]
        [Authorize]
        public IActionResult CreateCategory(Category category)
        {
            _categoryInterface.CreateCategory(category);
            return Ok();
        }
        [HttpGet("{id}/scripts")]
        public List<Script> ScriptsList(int id)
        {
            var scripts = _scriptInterface.GetScriptsForCategory(id);
            return scripts.ToList();
        }
    }
}