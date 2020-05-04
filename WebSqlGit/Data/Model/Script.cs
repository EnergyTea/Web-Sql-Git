using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebSqlGit.Model
{
    public class Script
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Body { get; set; }
        public DateTime DataTime { get; set; } 
    }
}
