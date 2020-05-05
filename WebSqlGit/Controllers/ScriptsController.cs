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
        public IActionResult CreateScript([FromForm]Script script)
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
