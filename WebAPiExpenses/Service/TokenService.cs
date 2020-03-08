using System;
using System.Security.Claims;
using System.Text;
using WebAPiExpenses.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace WebAPiExpenses.Service
{
    public static class TokenService
    {
        public static string GenerateToken(User user){

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Setting.Secret);
            
            var tokenDescriptor = new SecurityTokenDescriptor{
                    Subject = new ClaimsIdentity(
                        new Claim[]
                        {
                            new Claim(ClaimTypes.Name, user.Username.ToString()),
                            new Claim(ClaimTypes.Role, user.Role.ToString())
                        }),
                    Expires = DateTime.UtcNow.AddHours(2),
                    SigningCredentials = new SigningCredentials( new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}