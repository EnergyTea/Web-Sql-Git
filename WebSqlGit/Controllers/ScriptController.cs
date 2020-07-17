using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebSqlGit.Data.Interface;
using WebSqlGit.Model;

namespace WebSqlGit.Controllers
{
    [Route("api/scripts")]
    [ApiController]
    public class ScriptController : Controller
    {
        private readonly IScriptInterface _scriptInterface;
        public ScriptController(IScriptInterface scriptInterface)
        {
            _scriptInterface = scriptInterface;
        }

        [HttpGet]
        public List<Script> ScriptsList()
        {
            List<Script> scripts = _scriptInterface.GetAll().ToList();
            return scripts;
        }

        [HttpGet("user")]
        public List<Script> UserScripts()
        {
            string Author = User.Identity.Name;
            List<Script> scripts = _scriptInterface.GetUserScripts(Author).ToList();
            return scripts;
        }

        [HttpGet("{id}")]
        public IActionResult GetScript(int id)
        {
            string Author = User.Identity.Name;
            Script script = _scriptInterface.GetScript(id, Author);
            return Ok(script);
        }

        [HttpGet("{id}/history")]
        public IActionResult GetStoryScript(int id)
            {
            string Author = User.Identity.Name;
            Script script = _scriptInterface.GetScriptHistory(id, Author);
            return Ok(script);
        }

        [HttpGet("{id}/all")]
        public List<Script> GetStoriesScript(int id)
        {
            List<Script> scripts = _scriptInterface.GetScriptsHistory(id).ToList();
            return scripts;
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreateScript(Script script)
        {
            script.Author = User.Identity.Name;
            _scriptInterface.CreateScript(script);
            return Ok();
        }

        [HttpPost("{id}/delete")]
        [Authorize]
        public IActionResult DeleteScript(int id)
        {
            string Author = User.Identity.Name;
            _scriptInterface.DeleteScript(id, Author);
            return Ok();
        }

        [HttpPost("{id}/edit")]
        [Authorize]
        public IActionResult UpdateScript(Script script)
        {
            string Author = User.Identity.Name;
            _scriptInterface.UpdateScript(script, Author);
            return Ok();
        }

        [HttpGet("search/{pattern}")]
        public List<Script> GetScriptsBySearchPattern(string pattern)
        {
            return _scriptInterface.GetScriptsBySearchPattern(pattern);
        }
    }
}