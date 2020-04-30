using System;
using System.Collections.Generic;
using System.Linq;
using WebSqlGit.Data.Interface;
using WebSqlGit.Model;

namespace WebSqlGit.Data
{
    public class ScriptRepository : IScriptInterface
    {

        private List<Script> Scripts = new List<Script> { 
            
            new Script
            {
                Id = 1,
                Name = "Код для школы 1",
                Body = "sdwdazs12",                
            },
            new Script
            {
                Id = 2,
                Name = "Проверить 2",
                Body = "sdwda qq q qzs12",
            },
            new Script
            {
                Id = 3,
                Name = "В первую очередь 3",
                Body = "sd12312 1 1w12",
            },
            new Script
            {
                Id = 4,
                Name = " Ukfdyfz очередь 3",
                Body = "sdwda   aas zs12",
            },
            new Script
            {
                Id = 5,
                Name = "В первую 1",
                Body = "sdwdaasdasd zs12",
            },
            new Script
            {
                Id = 6,
                Name = "Free cods",
                Body = "sdwdazs1 sld ';lsm ';as;kdm flas 2",
            },
            new Script
            {
                Id = 7,
                Name = "SQL код  23",
                Body = "CREATE TABLE [dbo].[Author] ("
            }
        };              

        public List<Script> GetScripts(string name = null)
        {
            var scripts = Scripts.Select(s => new Script
            {
                Id = s.Id,
                Name = s.Name,
                Body = s.Body
            });
            if (!String.IsNullOrWhiteSpace(name))
            {
                scripts = scripts.Where(s => s.Name == name); // сделать conteins поиск 
            }
            return scripts.ToList();
        }

        public Script GetScript(int id)
        {
            var script = Scripts.FirstOrDefault(s => s.Id == id);
            if (script == null)
            {
                return null;
            }
            return new Script
            {
                Id = script.Id,
                Name = script.Name,
                Body = script.Body,
                DataTime = script.DataTime
            };
        }

        public Script CreateScript(string name = null, string body = null)
        {
            var script = new Script { Id = Scripts.Count > 0 ? Scripts.Max(s => s.Id) + 1 : 1, Name = name, Body = body };
            Scripts.Add(script);
            return new Script
            {
                Id = script.Id,
                Name = script.Name,
                Body = script.Body

            };
        }

        public Script DeleteScript(int id)
        {
            var script = Scripts.FirstOrDefault(s => s.Id == id);
            if (script == null)
            {
                return null;
            }
            Scripts = Scripts.Where(s => s.Id != id).ToList();
            return new Script { 
                Id = script.Id,
                Name = script.Name,
                Body = script.Body,
            };
        }

        public Script UpdateScript(int id, string name = null, string body = null)
        {
            Script script = Scripts.FirstOrDefault(s => s.Id == id);
            if (script == null)
            {
                return null;
            }
            if (!String.IsNullOrWhiteSpace(name))
            {
                script.Name = name;
            }
            if (!String.IsNullOrWhiteSpace(body))
            {
                script.Body = body;
            }
            return new Script
            {
                Id = script.Id,
                Name = script.Name,
                Body = script.Body,
                DataTime = script.DataTime
            };
            throw new NotImplementedException();
        }
    }
}
