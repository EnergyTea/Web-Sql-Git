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
        private readonly IScriptRepository _scriptInterface;
        public ScriptController( IScriptRepository scriptInterface )
        {
            _scriptInterface = scriptInterface;
        }

        [HttpGet]
        public List<Script> ScriptsList()
        {
            return _scriptInterface.GetAll();
        }

        [HttpGet( "user" )]
        public List<Script> UserScripts()
        {
            return _scriptInterface.GetUserScripts( User.Identity.Name );
        }

        [HttpGet( "{id}" )]
        public Script GetScript( int id )
        {
            return _scriptInterface.GetScript( id, User.Identity.Name );
        }

        [HttpGet( "{id}/history" )]
        public Script GetStoryScript( int id )
        {
            return _scriptInterface.GetScriptHistory( id, User.Identity.Name );
        }

        [HttpGet( "{id}/all" )]
        public List<Script> GetStoriesScript( int id )
        {
            return _scriptInterface.GetScriptsHistory( id );
        }

        [HttpPost]
        [Authorize]
        public void CreateScript( Script script )
        {
            script.Author = User.Identity.Name;
            _scriptInterface.CreateScript( script );
        }

        [HttpPost( "{id}/delete" )]
        [Authorize]
        public void DeleteScript( int id )
        {
            string Author = User.Identity.Name;
            _scriptInterface.DeleteScript( id, Author );
        }

        [HttpPost( "{id}/edit" )]
        [Authorize]
        public void UpdateScript( Script script )
        {
            string Author = User.Identity.Name;
            _scriptInterface.UpdateScript( script, Author );
        }

        [HttpGet( "search/{pattern}" )]
        public List<Script> GetScriptsBySearchPattern( string pattern )
        {
            return _scriptInterface.GetScriptsBySearchPattern( pattern );
        }
    }
}