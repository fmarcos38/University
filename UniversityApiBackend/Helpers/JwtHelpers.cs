using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using UniversityApiBackend.Models;

namespace UniversityApiBackend.Helpers
{
    static public class JwtHelpers
    {
        public static IEnumerable<Claim> GetClaims(this UserTokens userAccounts, Guid Id)
        {
            List<Claim> claims = new List<Claim> { 
                new Claim("Id", userAccounts.Id.ToString()),
                new Claim(ClaimTypes.Name, userAccounts.UserName),
                new Claim(ClaimTypes.Email, userAccounts.EmailId),
                new Claim(ClaimTypes.NameIdentifier, Id.ToString()),
                new Claim(ClaimTypes.Expiration, DateTime.UtcNow.AddDays(1).ToString("g")),
            };

            if(userAccounts.UserName == "Admin")
            {
                claims.Add(new Claim(ClaimTypes.Role, "Administrador"));
            }else if(userAccounts.UserName == "User")
            {
                claims.Add(new Claim(ClaimTypes.Role, "User"));
                claims.Add(new Claim("UserOnly", "User 1"));
            }
            return claims;
        }

        
        //llamo internamente a GetClaims pasandole el Usuario
        public static IEnumerable<Claim> GetClaims(this UserTokens userAccounts, out Guid Id)
        {
            Id = Guid.NewGuid();
            return GetClaims(userAccounts, out Id);
        }


        //obtengo el Token
        public static UserTokens GenTokenKey(UserTokens model, JwtSettings jwtSettings)
        {
            try
            {
                var userToken = new UserTokens();
                if(model == null)
                {
                    throw new ArgumentNullException(nameof(model));
                }

                //obtngo la clave secreta
                var key = System.Text.Encoding.ASCII.GetBytes(jwtSettings.IssuerSigningKey);

                //genero el Guid q expira en 1 dia
                Guid Id;

                DateTime expireTime = DateTime.UtcNow.AddDays(1);

                //genero la validez
                userToken.Validaty = expireTime.TimeOfDay;

                //Genero el JWT con toda la data antes creada
                var jwToken = new JwtSecurityToken(
                    issuer: jwtSettings.ValidIssuer,
                    audience: jwtSettings.ValidAudience,
                    claims: GetClaims(model, out Id),
                    notBefore: new DateTimeOffset(DateTime.Now).DateTime, //el tiempo en q no puede expirar al momento de crearse
                    expires: new DateTimeOffset(expireTime).DateTime,
                    signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256
                        ) //credenciales de firma
                    );

                //userToken
                userToken.Token = new JwtSecurityTokenHandler().WriteToken(jwToken);
                userToken.UserName= model.UserName;
                userToken.Id= model.Id;
                userToken.Guid = Id; // no se si da error?
                return userToken;

            }catch(Exception ex)
            {
                throw new Exception("Error al generar el Token", ex);
            }
        }
    }
}
