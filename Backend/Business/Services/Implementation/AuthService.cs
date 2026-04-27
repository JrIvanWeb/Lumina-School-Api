using LuminiSchool.Business.Exceptions;
using LuminiSchool.Business.Services.Contract;
using LuminiSchool.Domain.Entities.User;
using LuminiSchool.Domain.Model.User.DTOs;
using LuminiSchool.Infrastructure.JWT.Contract;
using LuminiSchool.Infrastructure.Repositories.Contract;
using Microsoft.AspNetCore.Identity;

namespace LuminiSchool.Business.Services.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser>  _um;
        private readonly RoleManager<ApplicationRole> _rm;
        private readonly IJwtService                  _jwt;
        private readonly IRefreshTokenRepository      _rtRepo;
        private readonly IPermissionRepository        _permRepo;

        public AuthService(
            UserManager<ApplicationUser>  um,
            RoleManager<ApplicationRole> rm,
            IJwtService                  jwt,
            IRefreshTokenRepository      rtRepo,
            IPermissionRepository        permRepo)
        {
            _um       = um;
            _rm       = rm;
            _jwt      = jwt;
            _rtRepo   = rtRepo;
            _permRepo = permRepo;
        }

        public async Task<LoginResponseDto> LoginAsync(LoginDto dto)
        {
            var user = await _um.FindByEmailAsync(dto.Email)
                       ?? throw new UnauthorizedException("Credenciales inválidas.");

            if (!user.IsActive)
                throw new UnauthorizedException("Cuenta desactivada. Contacta al administrador.");

            if (!await _um.CheckPasswordAsync(user, dto.Password))
                throw new UnauthorizedException("Credenciales inválidas.");

            return await BuildLoginResponseAsync(user);
        }

        public async Task<LoginResponseDto> RefreshTokenAsync(RefreshTokenRequestDto dto)
        {
            var stored = await _rtRepo.GetActiveAsync(dto.RefreshToken)
                         ?? throw new UnauthorizedException("Refresh token inválido o expirado.");

            var user = stored.User;
            if (!user.IsActive)
                throw new UnauthorizedException("Cuenta desactivada.");

            // Rotar: revocar el anterior y emitir uno nuevo
            var newRefresh = _jwt.GenerateRefreshToken();
            await _rtRepo.RevokeAsync(stored, newRefresh);

            var roles       = await _um.GetRolesAsync(user);
            var permissions = await GetPermissionsAsync(user, roles);

            var refreshToken = new RefreshToken
            {
                Token     = newRefresh,
                UserId    = user.Id,
                ExpiresAt = DateTime.UtcNow.AddDays(30)
            };
            await _rtRepo.AddAsync(refreshToken);
            await _rtRepo.SaveChangesAsync();

            return new LoginResponseDto
            {
                Token                 = _jwt.GenerateToken(user, roles, permissions),
                RefreshToken          = newRefresh,
                ExpiresAt             = DateTime.UtcNow.AddHours(8),
                RequiresPasswordChange = user.IsFirstLogin,
                User = MapUserInfo(user, roles)
            };
        }

        public async Task ChangePasswordAsync(Guid userId, ChangePasswordDto dto)
        {
            var user = await _um.FindByIdAsync(userId.ToString())
                       ?? throw new NotFoundException($"Usuario {userId} no encontrado.");

            var result = await _um.ChangePasswordAsync(user, dto.CurrentPassword, dto.NewPassword);
            if (!result.Succeeded)
                throw new BusinessException(string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        public async Task ForceChangePasswordAsync(Guid userId, ForceChangePasswordDto dto)
        {
            if (dto.NewPassword != dto.ConfirmPassword)
                throw new BusinessException("Las contraseñas no coinciden.");

            var user = await _um.FindByIdAsync(userId.ToString())
                       ?? throw new NotFoundException($"Usuario {userId} no encontrado.");

            if (!user.IsFirstLogin)
                throw new BusinessException("Este usuario ya cambió su contraseña inicial.");

            var token  = await _um.GeneratePasswordResetTokenAsync(user);
            var result = await _um.ResetPasswordAsync(user, token, dto.NewPassword);
            if (!result.Succeeded)
                throw new BusinessException(string.Join(", ", result.Errors.Select(e => e.Description)));

            user.IsFirstLogin = false;
            user.UpdatedAt    = DateTime.UtcNow;
            await _um.UpdateAsync(user);

            // Invalidar todos los refresh tokens anteriores por seguridad
            await _rtRepo.RevokeAllByUserAsync(userId);
            await _rtRepo.SaveChangesAsync();
        }

        // ── Helpers ───────────────────────────────────────────────────────────

        private async Task<LoginResponseDto> BuildLoginResponseAsync(ApplicationUser user)
        {
            var roles       = await _um.GetRolesAsync(user);
            var permissions = await GetPermissionsAsync(user, roles);
            var refreshStr  = _jwt.GenerateRefreshToken();

            // Revocar tokens anteriores y guardar el nuevo
            await _rtRepo.RevokeAllByUserAsync(user.Id);
            await _rtRepo.AddAsync(new RefreshToken
            {
                Token     = refreshStr,
                UserId    = user.Id,
                ExpiresAt = DateTime.UtcNow.AddDays(30)
            });
            await _rtRepo.SaveChangesAsync();

            return new LoginResponseDto
            {
                Token                 = _jwt.GenerateToken(user, roles, permissions),
                RefreshToken          = refreshStr,
                ExpiresAt             = DateTime.UtcNow.AddHours(8),
                RequiresPasswordChange = user.IsFirstLogin,
                User = MapUserInfo(user, roles)
            };
        }

        private async Task<IList<string>> GetPermissionsAsync(ApplicationUser user, IList<string> roles)
        {
            var permissions = new List<string>();
            foreach (var roleName in roles)
            {
                var role = await _rm.FindByNameAsync(roleName);
                if (role == null) continue;
                var perms = await _permRepo.GetPermissionNamesByRoleAsync(role.Id);
                permissions.AddRange(perms);
            }
            return permissions.Distinct().ToList();
        }

        private static UserInfoDto MapUserInfo(ApplicationUser user, IList<string> roles) =>
            new()
            {
                Id           = user.Id,
                FirstName    = user.FirstName,
                LastName     = user.LastName,
                FullName     = user.FullName,
                Email        = user.Email!,
                Roles        = roles,
                IsFirstLogin = user.IsFirstLogin
            };
    }
}
