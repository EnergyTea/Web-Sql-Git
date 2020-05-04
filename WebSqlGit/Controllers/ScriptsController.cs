using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebSqlGit.Data;
using WebSqlGit.Data.Interface;
using WebSqlGit.Model;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebSqlGit.Controllers
{
    [Route("api/scripts")]
    public class ScriptsController : ControllerBase
    {
        private readonly IScriptInterface _scriptInterface;
        // GET: /<controller>/
        public ScriptsController(IScriptInterface scriptInterface)
        {
            _scriptInterface = scriptInterface;
        }

        [HttpGet]
        public List<Script> ScriptsList(string name)
        {
            var scripts = _scriptInterface.GetAll(name);

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
        public IActionResult CreateScript([FromForm]Script script)
        {
            Script created = _scriptInterface.CreateScript(script.Name, script.Body);

            return Ok(created);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteScript(int id)
        {
            Script script = _scriptInterface.DeleteScript(id);
            if (script == null)
            {
                return NotFound();
            }
            return Ok(script);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateScreipt(int id, [FromForm]Script script)
        {
            Script updated = _scriptInterface.UpdateScript(id, script.Body, script.Body);
        if (updated == null)
            {
                return NotFound();
            }
            return Ok(updated);
        }
        // [HttpPost("")]

        // [Delete]

        // [Put]
    }
}
