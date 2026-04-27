using LuminiSchool.Business.Exceptions;
using LuminiSchool.Business.Services.Contract;
using LuminiSchool.Domain.Entities.User;
using LuminiSchool.Domain.Model.User.DTOs;
using LuminiSchool.Infrastructure.Repositories.Contract;
using Microsoft.AspNetCore.Identity;

namespace LuminiSchool.Business.Services.Implementation
{
    public class PermissionService : IPermissionService
    {
        private readonly UserManager<ApplicationUser>  _um;
        private readonly RoleManager<ApplicationRole> _rm;
        private readonly IPermissionRepository        _repo;

        public PermissionService(
            UserManager<ApplicationUser>  um,
            RoleManager<ApplicationRole> rm,
            IPermissionRepository        repo)
        {
            _um   = um;
            _rm   = rm;
            _repo = repo;
        }

        public async Task<UserPermissionsDto> GetUserPermissionsAsync(Guid userId)
        {
            var user = await _um.FindByIdAsync(userId.ToString())
                       ?? throw new NotFoundException($"Usuario {userId} no encontrado.");

            var roles       = await _um.GetRolesAsync(user);
            var permissions = new List<string>();

            foreach (var roleName in roles)
            {
                var role = await _rm.FindByNameAsync(roleName);
                if (role == null) continue;
                var perms = await _repo.GetPermissionNamesByRoleAsync(role.Id);
                permissions.AddRange(perms);
            }

            return new UserPermissionsDto
            {
                UserId      = userId,
                Roles       = roles,
                Permissions = permissions.Distinct().ToList()
            };
        }

        public async Task<IList<PermissionDto>> GetAllPermissionsAsync()
        {
            var perms = await _repo.GetAllAsync();
            return perms.Select(p => new PermissionDto
            {
                Id          = p.Id,
                Name        = p.Name,
                DisplayName = p.DisplayName,
                Module      = p.Module
            }).ToList();
        }

        public async Task AssignPermissionToRoleAsync(Guid roleId, Guid permissionId)
        {
            if (await _rm.FindByIdAsync(roleId.ToString()) == null)
                throw new NotFoundException($"Rol {roleId} no encontrado.");
            if (await _repo.GetByIdAsync(permissionId) == null)
                throw new NotFoundException($"Permiso {permissionId} no encontrado.");

            await _repo.AssignToRoleAsync(roleId, permissionId);
            await _repo.SaveChangesAsync();
        }

        public async Task RemovePermissionFromRoleAsync(Guid roleId, Guid permissionId)
        {
            await _repo.RemoveFromRoleAsync(roleId, permissionId);
            await _repo.SaveChangesAsync();
        }
    }
}
