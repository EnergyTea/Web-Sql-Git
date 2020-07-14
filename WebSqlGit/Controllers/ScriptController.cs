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
            var scripts = _scriptInterface.GetAll();
            return scripts.ToList();
        }

        [HttpGet("user")]
        public List<Script> UserScripts()
        {
            var Author = User.Identity.Name;
            var scripts = _scriptInterface.GetUserScripts(Author);
            return scripts.ToList();
        }

        [HttpGet("{id}")]
        public IActionResult GetScript(int id)
        {
            var Author = User.Identity.Name;
            Script script = _scriptInterface.GetScript(id, Author);
            return Ok(script);
        }

        [HttpGet("{id}/history")]
        public IActionResult GetStoryScript(int id)
            {
            var Author = User.Identity.Name;
            Script script = _scriptInterface.GetScriptHistory(id, Author);
            return Ok(script);
        }

        [HttpGet("{id}/all")]
        public List<Script> GetStoriesScript(int id)
        {
            var scripts = _scriptInterface.GetScriptsHistory(id);
            return scripts.ToList();
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
            var Author = User.Identity.Name;
            _scriptInterface.DeleteScript(id, Author);
            return Ok();
        }

        [HttpPost("{id}/edit")]
        [Authorize]
        public IActionResult UpdateScript(Script script)
        {
            var Author = User.Identity.Name;
            _scriptInterface.UpdateScript(script, Author);
            return Ok();
        }
    }
}