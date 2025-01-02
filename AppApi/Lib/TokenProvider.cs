using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.JsonWebTokens;
using AppApi.Data.Models;

namespace AppApi.Lib
{
    public sealed class TokenProvider (IConfiguration configuration) 
    {
        public string Create(User user)
        {
            // Get the secret key from the configuration
            var secretKey = configuration["Jwt:Secret"];
            if (string.IsNullOrEmpty(secretKey))
            {
                throw new InvalidOperationException("JWT Secret is not configured.");
            }

            // Create the security key
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            // Create the signing credentials
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            // Create the token descriptor
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    [
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                    new Claim("Role", "user") // TODO : Add roles to the user model
                    ]),
                Expires = DateTime.UtcNow.AddMinutes(configuration.GetValue<int>("Jwt:ExpirationInMinutes")),
                SigningCredentials = credentials,
                Issuer = configuration["Jwt:Issuer"],
                Audience = configuration["Jwt:Audience"],
                
            };

            // create the token
            var handler = new JsonWebTokenHandler();
            string token = handler.CreateToken(tokenDescriptor);
             
            return token;

        }


    }
}
