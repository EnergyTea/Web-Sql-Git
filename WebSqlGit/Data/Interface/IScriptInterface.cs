using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSqlGit.Model;

namespace WebSqlGit.Data.Interface
{
    public interface IScriptInterface
    {
        IEnumerable<Script> GetScripts(int CategoryId);
        List<Script> GetAll(string name);
        Script GetScript(int id);
        Script CreateScript(string name, string body);
        Script DeleteScript(int id);
        Script UpdateScript(int id, string name, string body);
    }
}
