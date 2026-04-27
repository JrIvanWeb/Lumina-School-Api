using LuminiSchool.Domain.Entities.User;

namespace LuminiSchool.Infrastructure.Repositories.Contract
{
    public interface IPermissionRepository
    {
        Task<IList<string>> GetPermissionNamesByRoleAsync(Guid roleId);
        Task<IList<Permission>> GetAllAsync();
        Task<Permission?> GetByIdAsync(Guid id);
        Task AddAsync(Permission permission);
        Task AssignToRoleAsync(Guid roleId, Guid permissionId);
        Task RemoveFromRoleAsync(Guid roleId, Guid permissionId);
        Task SaveChangesAsync();
    }
}
