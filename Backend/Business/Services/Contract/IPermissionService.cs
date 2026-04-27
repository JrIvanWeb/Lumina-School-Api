using LuminiSchool.Domain.Model.User.DTOs;

namespace LuminiSchool.Business.Services.Contract
{
    public interface IPermissionService
    {
        Task<UserPermissionsDto>  GetUserPermissionsAsync(Guid userId);
        Task<IList<PermissionDto>> GetAllPermissionsAsync();
        Task AssignPermissionToRoleAsync(Guid roleId, Guid permissionId);
        Task RemovePermissionFromRoleAsync(Guid roleId, Guid permissionId);
    }
}
