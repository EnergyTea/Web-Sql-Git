using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebSqlGit.Data.Interface;
using WebSqlGit.Data.Model;

namespace WebSqlGit.Controllers
{
    [Route( "api/accounts" )]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IUserRepository _userInterface;
                
        public AccountController( IUserRepository userInterface )
        {
            _userInterface = userInterface;
        }

        [HttpGet]
        public User GetUser()
        {
            User user = new User { };
            if ( User.Identity.Name == null )
            {
                return user;
            }
            user.Name = _userInterface.GetUserNameByLogin( User.Identity.Name );

            return user;
        }

        [HttpPost( "registration" )]
        public void CreateUser( User user )
        {
            _userInterface.RegistrationUser( user );
        }

        [HttpPost]
        public async Task Authorize( User user )             
        {
            User AuthorizeUser = _userInterface.Authorize( user ); 
            if ( AuthorizeUser != null ) 
            {
                var claims = new List<Claim>
                {
                    new Claim( ClaimsIdentity.DefaultNameClaimType, AuthorizeUser.Login )
                };
                ClaimsIdentity id = new ClaimsIdentity( claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType );
                // установка аутентификационных куки
                await HttpContext.SignInAsync( CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal( id ) );
            }
        }

        [HttpGet( "logout" )]
        public async void Logout()
        {
            await HttpContext.SignOutAsync( CookieAuthenticationDefaults.AuthenticationScheme );
        }
    }
}