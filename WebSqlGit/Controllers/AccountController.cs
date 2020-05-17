using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;
using WebSqlGit.Data.Interface;
using WebSqlGit.Data.Model;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Hosting;

namespace WebSqlGit.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : Controller
    {
    }
}