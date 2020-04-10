using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebSqlGit.Data.Model;

namespace WebSqlGit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScriptController : ControllerBase
    {
        [HttpGet("{id}")]
        public IActionResult GetScript(int id)
        {
            Script script = ScriptRepository.FindScript(id);
            if (script == null)
            {
                return NotFound();
            }
            return Ok(script);
        }

        [HttpPost]
        public IActionResult CreateScript([FromForm]Script script)
        {
            Script created = ScriptRepository.CreateScript(script.Code, script.Author);
            return Ok(created);
        }
        [HttpGet]
        public IActionResult GetScripts(string code, string author)
        {
            return Ok(ScriptRepository.FindScripts(code, author));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteScript(int id)
        {
            Script script = ScriptRepository.DeleteScript(id);
            if (script == null)
            {
                return NotFound();
            }
            return Ok(script);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateScript(int id, int Data, [FromForm]Script script)
        {
            Script updated = ScriptRepository.UpdateScript(id, Data, script.Code, script.Author);
            if (updated == null)
            {
                return NotFound();
            }
            return Ok(updated);
        }
    }
}