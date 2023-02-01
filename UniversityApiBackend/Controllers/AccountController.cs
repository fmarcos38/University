using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniversityApiBackend.DataAccess;
using UniversityApiBackend.Helpers;
using UniversityApiBackend.Models;

namespace UniversityApiBackend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UniversityDBContext _context;//acceso al context
        private readonly JwtSettings _jwtSettings;// declaro para JWT

        public AccountController(UniversityDBContext context, JwtSettings jwtSettings)//agrego param JWT
        {
            _context = context;//añado el context
            _jwtSettings = jwtSettings;//declaro en el constructor parapoder utilizar JWT
        }

        // Example Users
        // TODO: Change by real users in DB
        private IEnumerable<User> Logins = new List<User>()
        {
            new User()
            {
                Id = 1,
                Email = "martin@imaginagroup.com",
                Name = "Admin",
                Password = "Admin"
            },
            new User()
            {
                Id = 2,
                Email = "pepe@imaginagroup.com",
                Name = "User1",
                Password = "pepe"
            }
        };


        [HttpPost]
        public IActionResult GetToken(UserLogins userLogin)
        {

            try
            {

                var Token = new UserTokens();

                // TODO:
                // Search a user in context with LINQ
                var searchUser = (from user in _context.Users//accedo a la tabla Users de la DB
                                  where user.Name == userLogin.UserName && user.Password == userLogin.Password
                                  select user).FirstOrDefault();


                // TODO: Change to searchUser
                // var Valid = Logins.Any(user => user.Name.Equals(userLogin.UserName, StringComparison.OrdinalIgnoreCase));

                if (searchUser != null)
                {
                    // var user = Logins.FirstOrDefault(user => user.Name.Equals(userLogin.UserName, StringComparison.OrdinalIgnoreCase));

                    Token = JwtHelpers.GenTokenKey(new UserTokens()//genero token
                    {
                        UserName = searchUser.Name,
                        EmailId = searchUser.Email,
                        Id = searchUser.Id,
                        GuidId = Guid.NewGuid(),

                    }, _jwtSettings);

                }
                else
                {
                    return BadRequest("Wrong Password");
                }
                return Ok(Token);
            }
            catch (Exception ex)
            {
                throw new Exception("GetToken Error", ex);
            }
        }

        [HttpGet]
        //con la sgt line -> le doy derechos solo a los Admin
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrador")]
        public IActionResult GetUserList()
        {
            return Ok(Logins);
        }

    }
}
