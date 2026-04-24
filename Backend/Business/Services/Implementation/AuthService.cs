using LuminiSchool.Business.Exceptions;
using LuminiSchool.Business.Services.Contract;
using LuminiSchool.Domain.Entities.User;
using LuminiSchool.Domain.Model.User.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace LuminiSchool.Business.Services.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _um; private readonly IConfiguration _cfg;
        public AuthService(UserManager<ApplicationUser> um, IConfiguration cfg) { _um = um; _cfg = cfg; }

        public async Task<LoginResponseDto> LoginAsync(LoginDto dto)
        {
            var user = await _um.FindByEmailAsync(dto.Email) ?? throw new UnauthorizedException("Credenciales inválidas.");
            if (!user.IsActive) throw new UnauthorizedException("Cuenta desactivada.");
            if (!await _um.CheckPasswordAsync(user, dto.Password)) throw new UnauthorizedException("Credenciales inválidas.");
            var roles = await _um.GetRolesAsync(user);
            return new LoginResponseDto { Token = GenToken(user, roles), RefreshToken = GenRefresh(), ExpiresAt = DateTime.UtcNow.AddHours(8), User = new UserInfoDto { Id = user.Id, FirstName = user.FirstName, LastName = user.LastName, Email = user.Email!, Roles = roles } };
        }

        public async Task<UserInfoDto> RegisterAsync(RegisterUserDto dto)
        {
            if (await _um.FindByEmailAsync(dto.Email) != null) throw new BusinessException($"El correo {dto.Email} ya está registrado.");
            var user = new ApplicationUser { Id = Guid.NewGuid(), FirstName = dto.FirstName, LastName = dto.LastName, Email = dto.Email, UserName = dto.Email, IsActive = true, CreatedAt = DateTime.UtcNow };
            var result = await _um.CreateAsync(user, dto.Password);
            if (!result.Succeeded) throw new BusinessException(string.Join(", ", result.Errors.Select(e => e.Description)));
            await _um.AddToRoleAsync(user, dto.Role);
            return new UserInfoDto { Id = user.Id, FirstName = user.FirstName, LastName = user.LastName, Email = user.Email!, Roles = await _um.GetRolesAsync(user) };
        }

        public async Task ChangePasswordAsync(Guid userId, ChangePasswordDto dto)
        {
            var user = await _um.FindByIdAsync(userId.ToString()) ?? throw new NotFoundException($"Usuario {userId} no encontrado.");
            var r = await _um.ChangePasswordAsync(user, dto.CurrentPassword, dto.NewPassword);
            if (!r.Succeeded) throw new BusinessException(string.Join(", ", r.Errors.Select(e => e.Description)));
        }

        private string GenToken(ApplicationUser user, IList<string> roles)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_cfg["Jwt:Key"]!));
            var claims = new List<Claim> { new(ClaimTypes.NameIdentifier, user.Id.ToString()), new(ClaimTypes.Email, user.Email!), new(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"), new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) };
            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));
            var token = new JwtSecurityToken(issuer: _cfg["Jwt:Issuer"], audience: _cfg["Jwt:Audience"], claims: claims, expires: DateTime.UtcNow.AddHours(8), signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private static string GenRefresh() { var b = new byte[64]; RandomNumberGenerator.Fill(b); return Convert.ToBase64String(b); }
    }
}
