using System;
using System.Security.Claims;
using System.Text;
using WebAPiExpenses.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;

namespace WebAPiExpenses.Service
{
    public static class TokenService
    {

      
        // It was created Role claim, but it's not applied in this solution. It's implemented case needs to use in the future.        
        public static string GenerateToken(User user, string secretkey){

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretkey);
            
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