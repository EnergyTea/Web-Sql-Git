using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace WebSqlGit.Model
{
    public class Script
    {
        public int Id { get; set; }
        public int ScriptId { get; set; }
        public int CategoryId { get; set; }
        public int Version { get; set; } // Изменить на Даблл
        public string Name { get; set; }
        public string Body { get; set; }
        public string Author { get; set; }
        public DateTime CreationDataTime { get; set; } 
        public DateTime UpdateDataTime { get; set; } 
        public bool IsLastVersion { get; set; }
    }
}
