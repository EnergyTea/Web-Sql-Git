using System.Collections.Generic;
using WebSqlGit.Model;

namespace WebSqlGit.Data.Interface
{
    public interface IScriptInterface
    {
        List<Script> GetScriptsForCategory(int сategoryId);
        List<Script> GetScriptsHistory(int id);
        List<Script> GetAll();
        List<Script> GetUserScripts(string author);
        List<Script> GetScriptsBySearchPattern(string name);
        Script GetScriptHistory(int id, string author);
        Script GetScript(int id, string author);
        void CreateScript(Script script);
        void DeleteScript(int id, string author);
        void UpdateScript(Script script, string author);
    }
}
