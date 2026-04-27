using LuminiSchool.Domain.Entities.User;
using LuminiSchool.Infrastructure.Repositories.Contract;
using Microsoft.EntityFrameworkCore;

namespace LuminiSchool.Infrastructure.Repositories.Implementation
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly ApplicationDbContext _db;
        public PermissionRepository(ApplicationDbContext db) => _db = db;

        public async Task<IList<string>> GetPermissionNamesByRoleAsync(Guid roleId) =>
            await _db.RolePermissions
                     .Where(rp => rp.RoleId == roleId)
                     .Select(rp => rp.Permission.Name)
                     .ToListAsync();

        public async Task<IList<Permission>> GetAllAsync() =>
            await _db.Permissions.AsNoTracking().ToListAsync();

        public async Task<Permission?> GetByIdAsync(Guid id) =>
            await _db.Permissions.FindAsync(id);

        public async Task AddAsync(Permission permission) =>
            await _db.Permissions.AddAsync(permission);

        public async Task AssignToRoleAsync(Guid roleId, Guid permissionId)
        {
            var exists = await _db.RolePermissions
                                  .AnyAsync(rp => rp.RoleId == roleId && rp.PermissionId == permissionId);
            if (!exists)
                await _db.RolePermissions.AddAsync(new RolePermission { RoleId = roleId, PermissionId = permissionId });
        }

        public async Task RemoveFromRoleAsync(Guid roleId, Guid permissionId)
        {
            var rp = await _db.RolePermissions
                               .FirstOrDefaultAsync(x => x.RoleId == roleId && x.PermissionId == permissionId);
            if (rp != null) _db.RolePermissions.Remove(rp);
        }

        public async Task SaveChangesAsync() => await _db.SaveChangesAsync();
    }
}
