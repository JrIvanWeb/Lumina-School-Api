using LuminiSchool.Business.Exceptions;
using LuminiSchool.Business.Services.Contract;
using LuminiSchool.Domain.Entities.User;
using LuminiSchool.Domain.Model.User.DTOs;
using LuminiSchool.Infrastructure.Email.Contract;
using Microsoft.AspNetCore.Identity;

namespace LuminiSchool.Business.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _um;
        private readonly IEmailService               _email;

        public UserService(UserManager<ApplicationUser> um, IEmailService email)
        {
            _um    = um;
            _email = email;
        }

        public async Task<UserInfoDto> CreateUserAsync(CreateUserDto dto)
        {
            if (await _um.FindByEmailAsync(dto.Email) != null)
                throw new ConflictException($"El correo {dto.Email} ya está registrado.");

            // Generar contraseña temporal aleatoria (12 caracteres, cumple política)
            var tempPassword = GenerateTemporaryPassword();

            var user = new ApplicationUser
            {
                Id           = Guid.NewGuid(),
                FirstName    = dto.FirstName,
                LastName     = dto.LastName,
                Email        = dto.Email,
                UserName     = dto.Email,
                IsActive     = true,
                IsFirstLogin = true,
                CreatedAt    = DateTime.UtcNow
            };

            var result = await _um.CreateAsync(user, tempPassword);
            if (!result.Succeeded)
                throw new BusinessException(string.Join(", ", result.Errors.Select(e => e.Description)));

            var roleResult = await _um.AddToRoleAsync(user, dto.Role);
            if (!roleResult.Succeeded)
                throw new BusinessException($"Rol inválido: {dto.Role}");

            // Enviar contraseña temporal por correo
            await _email.SendTemporaryPasswordAsync(user.Email!, user.FullName, tempPassword);

            var roles = await _um.GetRolesAsync(user);
            return new UserInfoDto
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

        public async Task<IList<UserListItemDto>> GetAllUsersAsync()
        {
            var result = new List<UserListItemDto>();
            foreach (var user in _um.Users.Where(u => u.IsActive).OrderBy(u => u.LastName))
            {
                var roles = await _um.GetRolesAsync(user);
                result.Add(new UserListItemDto
                {
                    Id        = user.Id,
                    FullName  = user.FullName,
                    Email     = user.Email!,
                    Roles     = roles,
                    IsActive  = user.IsActive,
                    CreatedAt = user.CreatedAt
                });
            }
            return result;
        }

        public async Task ToggleActiveAsync(Guid userId)
        {
            var user = await _um.FindByIdAsync(userId.ToString())
                       ?? throw new NotFoundException($"Usuario {userId} no encontrado.");
            user.IsActive  = !user.IsActive;
            user.UpdatedAt = DateTime.UtcNow;
            await _um.UpdateAsync(user);
        }

        // ── Helpers ───────────────────────────────────────────────────────────

        private static string GenerateTemporaryPassword()
        {
            const string upper   = "ABCDEFGHJKLMNPQRSTUVWXYZ";
            const string lower   = "abcdefghijkmnpqrstuvwxyz";
            const string digits  = "23456789";
            const string special = "!@#$%";
            const string all     = upper + lower + digits + special;

            var rng   = new Random();
            var chars = new char[12];
            chars[0]  = upper[rng.Next(upper.Length)];
            chars[1]  = lower[rng.Next(lower.Length)];
            chars[2]  = digits[rng.Next(digits.Length)];
            chars[3]  = special[rng.Next(special.Length)];
            for (int i = 4; i < 12; i++)
                chars[i] = all[rng.Next(all.Length)];

            return new string(chars.OrderBy(_ => rng.Next()).ToArray());
        }
    }
}
