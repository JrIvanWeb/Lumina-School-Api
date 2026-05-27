// ============================================================
// RUTA: Backend/Business/Services/Implementation/UserService.cs
// ACCIÓN: REEMPLAZA el archivo existente completamente
// ============================================================
using LuminiSchool.Business.Exceptions;
using LuminiSchool.Business.Services.Contract;
using LuminiSchool.Domain.Entities.User;
using LuminiSchool.Domain.Model.User.DTOs;
using LuminiSchool.Infrastructure.Email.Contract;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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

        // ── Crear usuario ─────────────────────────────────────────────────────
        public async Task<UserInfoDto> CreateUserAsync(CreateUserDto dto)
        {
            if (await _um.FindByEmailAsync(dto.Email) != null)
                throw new ConflictException($"El correo {dto.Email} ya está registrado.");

            var tempPassword = GenerateTemporaryPassword();

            var user = new ApplicationUser
            {
                Id             = Guid.NewGuid(),
                FirstName      = dto.FirstName,
                LastName       = dto.LastName,
                Email          = dto.Email,
                UserName       = dto.Email,         // username inicial = email
                DocumentNumber = dto.DocumentNumber?.Trim(),
                IsActive       = true,
                IsFirstLogin   = true,
                HasCredentials = false,
                CreatedAt      = DateTime.UtcNow
            };

            var result = await _um.CreateAsync(user, tempPassword);
            if (!result.Succeeded)
                throw new BusinessException(string.Join(", ", result.Errors.Select(e => e.Description)));

            var roleResult = await _um.AddToRoleAsync(user, dto.Role);
            if (!roleResult.Succeeded)
                throw new BusinessException($"Rol inválido: {dto.Role}");

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

        // ── Listar usuarios ───────────────────────────────────────────────────
        // CAMBIO: incluye DocumentNumber, HasCredentials y Username en la respuesta
        public async Task<IList<UserListItemDto>> GetAllUsersAsync()
        {
            var users = await _um.Users
                .OrderBy(u => u.LastName)
                .ToListAsync();

            var result = new List<UserListItemDto>();
            foreach (var user in users)
            {
                var roles = await _um.GetRolesAsync(user);
                result.Add(new UserListItemDto
                {
                    Id             = user.Id,
                    FullName       = user.FullName,
                    Email          = user.Email!,
                    Roles          = roles,
                    IsActive       = user.IsActive,
                    CreatedAt      = user.CreatedAt,
                    DocumentNumber = user.DocumentNumber,
                    HasCredentials = user.HasCredentials,
                    Username       = user.HasCredentials ? user.UserName : null
                });
            }
            return result;
        }

        // ── Actualizar usuario ────────────────────────────────────────────────
        public async Task<UserInfoDto> UpdateUserAsync(Guid userId, UpdateUserDto dto)
        {
            var user = await _um.FindByIdAsync(userId.ToString())
                       ?? throw new NotFoundException($"Usuario {userId} no encontrado.");

            user.FirstName      = dto.FirstName;
            user.LastName       = dto.LastName;
            user.UpdatedAt      = DateTime.UtcNow;

            if (!string.IsNullOrWhiteSpace(dto.DocumentNumber))
                user.DocumentNumber = dto.DocumentNumber.Trim();

            var updateResult = await _um.UpdateAsync(user);
            if (!updateResult.Succeeded)
                throw new BusinessException(string.Join(", ", updateResult.Errors.Select(e => e.Description)));

            // Cambiar rol si es diferente
            var currentRoles = await _um.GetRolesAsync(user);
            if (!currentRoles.Contains(dto.Role))
            {
                await _um.RemoveFromRolesAsync(user, currentRoles);
                var addResult = await _um.AddToRoleAsync(user, dto.Role);
                if (!addResult.Succeeded)
                    throw new BusinessException($"Rol inválido: {dto.Role}");
            }

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

        // ── Eliminar (soft delete) ────────────────────────────────────────────
        public async Task DeleteUserAsync(Guid userId)
        {
            var user = await _um.FindByIdAsync(userId.ToString())
                       ?? throw new NotFoundException($"Usuario {userId} no encontrado.");

            user.IsActive  = false;
            user.UpdatedAt = DateTime.UtcNow;
            var result = await _um.UpdateAsync(user);
            if (!result.Succeeded)
                throw new BusinessException(string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        // ── Toggle activo ─────────────────────────────────────────────────────
        public async Task ToggleActiveAsync(Guid userId)
        {
            var user = await _um.FindByIdAsync(userId.ToString())
                       ?? throw new NotFoundException($"Usuario {userId} no encontrado.");
            user.IsActive  = !user.IsActive;
            user.UpdatedAt = DateTime.UtcNow;
            await _um.UpdateAsync(user);
        }

        // ── Asignar rol ───────────────────────────────────────────────────────
        // NUEVO: requerido por PATCH /api/users/{id}/role
        public async Task AssignRoleAsync(Guid userId, string role)
        {
            var user = await _um.FindByIdAsync(userId.ToString())
                       ?? throw new NotFoundException($"Usuario {userId} no encontrado.");

            var currentRoles = await _um.GetRolesAsync(user);
            if (!currentRoles.Contains(role))
            {
                await _um.RemoveFromRolesAsync(user, currentRoles);
                var result = await _um.AddToRoleAsync(user, role);
                if (!result.Succeeded)
                    throw new BusinessException($"Rol inválido: {role}");
            }
        }

        // ── Generar credenciales ──────────────────────────────────────────────
        // NUEVO: requerido por POST /api/users/{id}/generate-credentials
        public async Task<GenerateCredentialsResultDto> GenerateCredentialsAsync(
            Guid userId, GenerateCredentialsDto dto)
        {
            var user = await _um.FindByIdAsync(userId.ToString())
                       ?? throw new NotFoundException($"Usuario {userId} no encontrado.");

            if (string.IsNullOrWhiteSpace(dto.DocumentNumber))
                throw new BusinessException("El número de documento es requerido.");

            var newUsername    = dto.DocumentNumber.Trim();
            var tempPassword   = GenerateTemporaryPassword();

            // Cambiar username al número de documento
            user.UserName       = newUsername;
            user.NormalizedUserName = newUsername.ToUpperInvariant();
            user.DocumentNumber = newUsername;
            user.HasCredentials = true;
            user.UpdatedAt      = DateTime.UtcNow;

            var updateResult = await _um.UpdateAsync(user);
            if (!updateResult.Succeeded)
                throw new BusinessException(string.Join(", ", updateResult.Errors.Select(e => e.Description)));

            // Resetear contraseña con token
            var token  = await _um.GeneratePasswordResetTokenAsync(user);
            var pwResult = await _um.ResetPasswordAsync(user, token, tempPassword);
            if (!pwResult.Succeeded)
                throw new BusinessException(string.Join(", ", pwResult.Errors.Select(e => e.Description)));

            // Forzar cambio en próximo login
            user.IsFirstLogin = true;
            await _um.UpdateAsync(user);

            string? returnedPassword = null;

            if (dto.SendByEmail)
                await _email.SendTemporaryPasswordAsync(user.Email!, user.FullName, tempPassword);
            else
                returnedPassword = tempPassword;   // solo se devuelve si NO se envía por correo

            return new GenerateCredentialsResultDto
            {
                Username          = newUsername,
                TemporaryPassword = returnedPassword,
                EmailSent         = dto.SendByEmail
            };
        }

        // ── Reenviar credenciales ─────────────────────────────────────────────
        // NUEVO: requerido por POST /api/users/{id}/resend-credentials
        public async Task ResendCredentialsAsync(Guid userId)
        {
            var user = await _um.FindByIdAsync(userId.ToString())
                       ?? throw new NotFoundException($"Usuario {userId} no encontrado.");

            if (!user.HasCredentials)
                throw new BusinessException("Este usuario no tiene credenciales generadas aún.");

            var tempPassword = GenerateTemporaryPassword();

            var token    = await _um.GeneratePasswordResetTokenAsync(user);
            var pwResult = await _um.ResetPasswordAsync(user, token, tempPassword);
            if (!pwResult.Succeeded)
                throw new BusinessException(string.Join(", ", pwResult.Errors.Select(e => e.Description)));

            user.IsFirstLogin = true;
            await _um.UpdateAsync(user);

            await _email.SendTemporaryPasswordAsync(user.Email!, user.FullName, tempPassword);
        }

        // ── Helper ────────────────────────────────────────────────────────────
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
