using Blog.Extensions;
using Blog.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Blog.Services
{
    public class TokenService
    {
        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            //Cria uma isntância do token handler
            //utilizado para gerar de fato o token

            var key = Encoding.ASCII.GetBytes(Configuration.JwtKey);
            //Gera a chave

            var claims = user.GetClaims();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(key),
                            SecurityAlgorithms.HmacSha256Signature)

            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            //criou o token 
            return tokenHandler.WriteToken(token);
            //retorna tudo como STRING
        }
    }
}
