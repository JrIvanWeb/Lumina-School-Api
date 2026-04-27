using LuminiSchool.Domain.Entities.User;
using LuminiSchool.Infrastructure.JWT.Contract;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace LuminiSchool.Infrastructure.JWT.Implementation
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _cfg;
        public JwtService(IConfiguration cfg) => _cfg = cfg;

        public string GenerateToken(ApplicationUser user, IList<string> roles, IList<string> permissions)
        {
            var key    = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_cfg["Jwt:Key"]!));
            var expiry = DateTime.UtcNow.AddHours(double.Parse(_cfg["Jwt:ExpiresInHours"] ?? "8"));

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Email,          user.Email!),
                new(ClaimTypes.Name,           user.FullName),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));
            // Los permisos viajan como claims para que Angular los pueda leer desde el JWT
            claims.AddRange(permissions.Select(p => new Claim("permission", p)));

            var token = new JwtSecurityToken(
                issuer:             _cfg["Jwt:Issuer"],
                audience:           _cfg["Jwt:Audience"],
                claims:             claims,
                expires:            expiry,
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            var bytes = new byte[64];
            RandomNumberGenerator.Fill(bytes);
            return Convert.ToBase64String(bytes);
        }
    }
}
