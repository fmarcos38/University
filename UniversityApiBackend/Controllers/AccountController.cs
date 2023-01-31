using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniversityApiBackend.Helpers;
using UniversityApiBackend.Models;

namespace UniversityApiBackend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        //llamo la clase settings Q yo cree 
        private readonly JwtSettings _jwtSettings;

        //creo constructor
        public AccountController(JwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }


        //metodos/funciones
        private IEnumerable<User> Logins = new List<User>()
        {
            new User()
            {
                Id = 1,
                Email = "pp@hot.com",
                Name = "Admin",
                Password = "Admin",
            },
            new User()
            {
                Id = 2,
                Email = "tt@hot.com",
                Name = "User1",
                Password = "tt",
            }
        };


        //creo ruta de tipo post
        [HttpPost]
        public IActionResult GetToken(UserLogins userLogin) {

            try {
                var Token = new UserTokens();
                //en la variable Valid nos dice Si hay un user dentro de la lista q Coincide el nombre(retorna true o false)
                var Valid = Logins.Any(user => 
                user.Name.Equals(userLogin.UserName, StringComparison.OrdinalIgnoreCase));

                if (Valid)
                {
                    //aca guardo el user encntrado
                    var user = Logins.FirstOrDefault(user =>
                    user.Name.Equals(userLogin.UserName, StringComparison.OrdinalIgnoreCase));

                    //genero el token
                    Token = JwtHelpers.GenTokenKey(
                        new UserTokens() { 
                            UserName = user.Name,
                            EmailId = user.Email,
                            Id = user.Id,
                            GuidId = Guid.NewGuid(),
                        },
                        _jwtSettings
                        );

                }
                else
                {
                    return BadRequest("Pass incorrecto");
                }

                //si todo fue bien
                return Ok(Token);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en Token", ex);
            } 
        }

        //me traaigo todo los user
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrador")]
        public IActionResult GetUsersList()
        {
            return Ok(Logins);
        }
    }
}
