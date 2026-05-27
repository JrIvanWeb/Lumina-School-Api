// ============================================================
// RUTA: Backend/Business/Services/Contract/IUserService.cs
// ACCIÓN: REEMPLAZA el archivo existente completamente
// ============================================================
using LuminiSchool.Domain.Model.User.DTOs;

namespace LuminiSchool.Business.Services.Contract
{
    public interface IUserService
    {
        // Existentes
        Task<UserInfoDto>            CreateUserAsync(CreateUserDto dto);
        Task<UserInfoDto>            UpdateUserAsync(Guid userId, UpdateUserDto dto);
        Task                         DeleteUserAsync(Guid userId);
        Task<IList<UserListItemDto>> GetAllUsersAsync();
        Task                         ToggleActiveAsync(Guid userId);

        // ── Nuevos (requeridos por el frontend) ──────────────────────────────
        Task                             AssignRoleAsync(Guid userId, string role);
        Task<GenerateCredentialsResultDto> GenerateCredentialsAsync(Guid userId, GenerateCredentialsDto dto);
        Task                             ResendCredentialsAsync(Guid userId);
    }
}
