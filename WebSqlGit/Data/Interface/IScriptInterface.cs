using System.Collections.Generic;
using WebSqlGit.Model;

namespace WebSqlGit.Data.Interface
{
    public interface IScriptInterface
    {
        List<Script> GetScriptsForCategory(int CategoryId);
        List<Script> GetScriptsHistory(int id);
        List<Script> GetAll();
        List<Script> GetUserScripts(string Author);
        List<string> GetSearch(string Search);
        Script GetScriptHistory(int id, string Author);
        Script GetScript(int id, string Author);
        void CreateScript(Script script);
        void DeleteScript(int id, string Author);
        void UpdateScript(Script script, string Author);
    }
}
