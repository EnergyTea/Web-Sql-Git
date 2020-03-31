using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebSqlGit.Controllers
{
    public class BeerController : Controller
    {
        [HttpPost]
        public string BeerList(List<string> beers)
        {
            string beer = "";
            for (int i = 0; i < beers.Count; i++)
            {
                beer += beers[i] + ";  ";
            }
            return beer;
        }
    }  
}