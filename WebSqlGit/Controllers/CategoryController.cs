using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebSqlGit.Data.Interface;
using WebSqlGit.Data.Model;
using WebSqlGit.Model;

namespace WebSqlGit.Controllers
{
    [Route( "api/categories" )]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryInterface;
        private readonly IScriptRepository _scriptInterface;

        public CategoryController( ICategoryRepository categoryInterface, IScriptRepository scriptInterface )
        {
            _categoryInterface = categoryInterface;
            _scriptInterface = scriptInterface;
        }

        [HttpGet( "{id}" )]
        public Category GetCategory( int id )
        {
            return _categoryInterface.GetCategory( id );
        }

        [HttpGet]
        public List<Category> CategoryList()
        {
            return _categoryInterface.GetAll();
        }

        [HttpPost]
        [Authorize]
        public void CreateCategory( Category category )
        {
            _categoryInterface.CreateCategory( category );
        }

        [HttpGet( "{id}/scripts" )]
        public List<Script> ScriptsList( int id )
        {
            return _scriptInterface.GetScriptsForCategory( id );
        }
    }
}