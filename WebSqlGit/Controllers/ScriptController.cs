using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebSqlGit.Data.Interface;
using WebSqlGit.Model;

namespace WebSqlGit.Controllers
{
    [Route( "api/scripts" )]
    [ApiController]
    public class ScriptController : Controller
    {
        private readonly IScriptRepository _scriptRepository;

        public ScriptController( IScriptRepository scriptRepository )
        {
            _scriptRepository = scriptRepository;
        }

        [HttpGet]
        public List<Script> ScriptsList()
        {
            return _scriptRepository.GetAll();
        }

        [HttpGet( "user" )]
        public List<Script> UserScripts()
        {
            return _scriptRepository.GetUserScripts( User.Identity.Name );
        }

        [HttpGet( "{id}" )]
        public Script GetScript( int id )
        {
            return _scriptRepository.GetScript( id, User.Identity.Name );
        }

        [HttpGet( "{id}/history" )]
        public Script GetStoryScript( int id )
        {
            return _scriptRepository.GetScriptHistory( id, User.Identity.Name );
        }

        [HttpGet( "{id}/all" )]
        public List<Script> GetStoriesScript( int id )
        {
            return _scriptRepository.GetScriptsHistory( id );
        }

        [HttpPost]
        [Authorize]
        public void CreateScript( Script script )
        {
            script.Author = User.Identity.Name;
            _scriptRepository.CreateScript( script );
        }

        [HttpPost( "{id}/delete" )]
        [Authorize]
        public void DeleteScript( int id )
        {
            string Author = User.Identity.Name;
            _scriptRepository.DeleteScript( id, Author );
        }

        [HttpPost( "{id}/edit" )]
        [Authorize]
        public void UpdateScript( Script script )
        {
            string Author = User.Identity.Name;
            _scriptRepository.UpdateScript( script, Author );
        }

        [HttpGet( "search/{pattern}" )]
        public List<Script> GetScriptsBySearchPattern( string pattern )
        {
            return _scriptRepository.GetScriptsBySearchPattern( pattern );
        }
    }
}