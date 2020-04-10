using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebSqlGit.Data.Model
{
    public class Script
    {
        public int? Id { get; set; }
        public string Author { get; set; }
        public string Code { get; set; }
        public int Data { get; set; }
    }
}
