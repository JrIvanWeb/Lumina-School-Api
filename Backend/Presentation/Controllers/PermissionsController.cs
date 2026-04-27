using LuminiSchool.Business.Services.Contract;
using LuminiSchool.Domain.Entities.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LuminiSchool.Presentation.Controllers
{
    [ApiController]
    [Route("api/permissions")]
    [Authorize]
    public class PermissionsController : ControllerBase
    {
        private readonly IPermissionService _svc;
        public PermissionsController(IPermissionService svc) => _svc = svc;

        /// <summary>
        /// Devuelve roles y permisos del usuario autenticado.
        /// Angular lo usa para mostrar/ocultar módulos dinámicamente.
        /// </summary>
        [HttpGet("me")]
        public async Task<IActionResult> GetMyPermissions() =>
            Ok(await _svc.GetUserPermissionsAsync(UserId()));

        /// <summary>Listar todos los permisos del sistema.</summary>
        [HttpGet]
        [Authorize(Roles = $"{ApplicationRole.Roles.SuperAdmin},{ApplicationRole.Roles.Admin}")]
        public async Task<IActionResult> GetAll() =>
            Ok(await _svc.GetAllPermissionsAsync());

        /// <summary>Asignar un permiso a un rol.</summary>
        [HttpPost("roles/{roleId:guid}/permissions/{permissionId:guid}")]
        [Authorize(Roles = ApplicationRole.Roles.SuperAdmin)]
        public async Task<IActionResult> Assign(Guid roleId, Guid permissionId)
        {
            await _svc.AssignPermissionToRoleAsync(roleId, permissionId);
            return NoContent();
        }

        /// <summary>Quitar un permiso de un rol.</summary>
        [HttpDelete("roles/{roleId:guid}/permissions/{permissionId:guid}")]
        [Authorize(Roles = ApplicationRole.Roles.SuperAdmin)]
        public async Task<IActionResult> Remove(Guid roleId, Guid permissionId)
        {
            await _svc.RemovePermissionFromRoleAsync(roleId, permissionId);
            return NoContent();
        }

        private Guid UserId() =>
            Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    }
}
