using IdentityService.Configurations;
using IdentityService.Interfaces;
using IdentityService.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IdentityService.Services
{
    public class JwtService : IJwtService
    {
        private readonly JwtSettings _jwtsettings;

        public JwtService(IOptions<JwtSettings> options)
        {
            _jwtsettings = options.Value;    
        }
        public string GenerateToken(User user)
        {
            Console.WriteLine($"Expiry: {_jwtsettings.ExpiresInMinutes}");
            Console.WriteLine($"Now (UTC): {DateTime.UtcNow}");
            Console.WriteLine($"Expires (UTC): {DateTime.UtcNow.AddMinutes(_jwtsettings.ExpiresInMinutes)}");

            //Generating Claims that will be used by Other Service to Check Role=basic , Admin and provide access to Service
            //1. Payload 
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) //JTI ---> Generate unique ID for every Token
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtsettings.Key)
                );

            var credentials = new SigningCredentials(
                  key,
                  SecurityAlgorithms.HmacSha256
                );

            var token = new JwtSecurityToken(
                issuer: _jwtsettings.Issuer,
                audience: _jwtsettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtsettings.ExpiresInMinutes),
                signingCredentials: credentials

                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
