using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSqlGit.Data.Model;

namespace WebSqlGit
{
    public static class ScriptRepository
    {
        public static Script DeleteScript(int id)
        {
            var script = Scripts.FirstOrDefault(s => s.Id == id);
            if (script == null)
            {
                return null;
            }
            Scripts = Scripts.Where(s => s.Id != id).ToList();
            return new Script
            {
                Id = script.Id,
                Author = script.Author,
                Code = script.Code,
                Data = script.Data
            };
        }

        public static Script UpdateScript(int id, int data, string author = null, string code = null) // автора нужно автоматичкески присваивать USER
        {
            ScriptDto script = Scripts.FirstOrDefault(s => s.Id == id);
            if (script == null)
            {
                return null;
            }
            if (!String.IsNullOrWhiteSpace(code))
            {
                script.Code = code;
            }
            if (!String.IsNullOrWhiteSpace(author))
            {
                script.Author = author;
            }
            script.Data = data;
            return new Script
            {
                Id = script.Id,
                Code = script.Code,
                Author = script.Author,
                Data = script.Data
            };

        }

        public static Script CreateScript(string code = null, string author = null)
        {
            var script = new ScriptDto { Id = Scripts.Count > 0 ? Scripts.Max(b => b.Id) + 1 : 1, Code = code, Author = author };
            Scripts.Add(script);
            return new Script
            {
                Id = script.Id,
                Code = script.Code,
                Author = script.Author
            };
        }

        public static Script FindScript(int id)
        {
            var script = Scripts.FirstOrDefault(s => s.Id == id);
            if (script == null)
            {
                return null;
            }
            return new Script
            {
                Id = script.Id,
                Author = script.Author,
                Code = script.Code
            };
        }


        public static List<Script> FindScripts(string code = null, string author = null)
        {
            var result = Scripts.Select(s => new Script
            {
                Id = s.Id,
                Author = s.Author,
                Code = s.Code
            });
            if (!String.IsNullOrWhiteSpace(code))
            {
                result = result.Where(s => s.Code == code);
            }
            if (!String.IsNullOrWhiteSpace(author))
            {
                result = result.Where(s => s.Author == author);
            }
            return result.ToList();
        }

        private static List<ScriptDto> Scripts = new List<ScriptDto>();

        private class ScriptDto
        {
            public int Id { get; set; }
            public string Author { get; set; }
            public string Code { get; set; }
            public int Data { get; set; }
        }
    }
}
