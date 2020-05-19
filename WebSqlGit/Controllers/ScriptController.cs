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
        // GET: /<controller>/
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
        [Authorize]
        public IActionResult GetScript(int id)
        {
            Script script = _scriptInterface.GetScript(id);
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
        public IActionResult CreateScript(Script script)
        {
            _scriptInterface.CreateScript(script);
            return Ok();
        }

        [HttpPost("{id}/delete")]
        public IActionResult DeleteScript(int id)
        {
            _scriptInterface.DeleteScript(id);
            return Ok();
        }

        [HttpPost("version/{id}/delete")]
        public IActionResult DeleteVersionScript(int id)
        {
            _scriptInterface.DeleteVersionScript(id);
            return Ok();
        }

        [HttpPost("{id}/edit")]
        public IActionResult UpdateScreipt(Script script)
        {
            _scriptInterface.UpdateScript(script);
            return Ok();
        }
    }
}