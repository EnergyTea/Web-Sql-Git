using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebSqlGit.Data.Interface;
using WebSqlGit.Model;

namespace WebSqlGit.Controllers
{
    [Route("api/category/script")]
    [ApiController]
    public class CategoryScriptController : ControllerBase
    {
        private readonly IScriptInterface _scriptInterface;

        public CategoryScriptController(IScriptInterface scriptInterface)
        {
            _scriptInterface = scriptInterface;
        }


        [HttpGet("{id}")]
        public List<Script> ScriptsList(int CategoryId)
        {
            var scripts = _scriptInterface.GetScripts(CategoryId);
            return scripts.ToList();
        }
    }
}