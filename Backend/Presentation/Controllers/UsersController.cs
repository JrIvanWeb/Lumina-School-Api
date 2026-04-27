using LuminiSchool.Business.Services.Contract;
using LuminiSchool.Domain.Entities.User;
using LuminiSchool.Domain.Model.User.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LuminiSchool.Presentation.Controllers
{
    [ApiController]
    [Route("api/users")]
    [Authorize(Roles = $"{ApplicationRole.Roles.SuperAdmin},{ApplicationRole.Roles.Admin}")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _svc;
        public UsersController(IUserService svc) => _svc = svc;

        /// <summary>
        /// Crear un nuevo usuario. El sistema genera y envía la contraseña temporal por correo.
        /// Solo SuperAdmin y Admin pueden usar este endpoint.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto dto) =>
            Ok(await _svc.CreateUserAsync(dto));

        /// <summary>Listar todos los usuarios activos.</summary>
        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _svc.GetAllUsersAsync());

        /// <summary>Activar / desactivar un usuario.</summary>
        [HttpPatch("{id:guid}/toggle-active")]
        public async Task<IActionResult> ToggleActive(Guid id)
        {
            await _svc.ToggleActiveAsync(id);
            return NoContent();
        }
    }
}
