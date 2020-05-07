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
        public IActionResult GetScript(int id)
        {
            Script script = _scriptInterface.GetScript(id);
            if (script == null)
            {
                return NotFound();
            }
            return Ok(script);
        }

        [HttpPost]
        public IActionResult CreateScript(Script script)
        {
            _scriptInterface.CreateScript(script);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteScript(int id)
        {
            _scriptInterface.DeleteScript(id);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateScreipt([FromForm]Script script)
        {
            _scriptInterface.UpdateScript(script);
            return Ok();
        }
    }
}