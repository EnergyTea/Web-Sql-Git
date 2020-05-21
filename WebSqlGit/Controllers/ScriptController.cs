using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebSqlGit.Data.Interface;
using WebSqlGit.Data.Model;
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

        [HttpGet("{id}")]
        public IActionResult GetScript(int id)
        {
            var Author = User.Identity.Name;
            Script script = _scriptInterface.GetScript(id, Author);
            return Ok(script);
        }

        [HttpGet("{id}/history")]
        public IActionResult GetScriptHistory(int id)
        {
            Script script = _scriptInterface.GetScriptsHistory(id);
            return Ok(script);
        }

        [HttpGet("{id}/all")]
        public List<Script> GetScriptsOne(int id)
        {
            var scripts = _scriptInterface.GetScriptsOne(id);
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

        [HttpPost("version/{id}/delete")]
        [Authorize]
        public IActionResult DeleteVersionScript(int id)
        {
            var Author = User.Identity.Name;
            _scriptInterface.DeleteVersionScript(id, Author);
            return Ok();
        }

        [HttpPost("{id}/edit")]
        [Authorize]
        public IActionResult UpdateScreipt(Script script)
        {
            var Author = User.Identity.Name;
            _scriptInterface.UpdateScript(script, Author);
            return Ok();
        }
    }
}