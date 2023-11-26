using Microsoft.EntityFrameworkCore;
using System.Data;
using Varejao.Data;
using Varejao.Models;
using Varejao.Repositorios.Interface;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Varejao.Services
{
    public static class TokenService
    {
        public static readonly string Key = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

        public static string GenerateToken(LoggedUserModel user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(Key);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Nome),
                    new Claim("Apelido", user.Apelido),
                    new Claim("Id", user.Id.ToString()),
                    new Claim("Nivel", user.Nivel),
                    new Claim(ClaimTypes.Email, user.Email ?? string.Empty)
                }),
                Expires = DateTime.Now.AddHours(12),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            tokenDescriptor.Subject.AddClaims(user.Roles.Select(a => new Claim(ClaimTypes.Role, a)));

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
