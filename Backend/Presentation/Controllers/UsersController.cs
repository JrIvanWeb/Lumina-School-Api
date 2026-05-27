using LuminiSchool.Business.Services.Contract;
using LuminiSchool.Domain.Model.User.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace LuminiSchool.Presentation.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _svc;

        public UsersController(IUserService svc) => _svc = svc;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try { return Ok(await _svc.GetAllUsersAsync()); }
            catch (Exception ex) { return StatusCode(500, new { message = "Error obteniendo usuarios", detail = ex.Message }); }
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto dto)
        {
            try { return Ok(await _svc.CreateUserAsync(dto)); }
            catch (Exception ex) { return StatusCode(500, new { message = ex.Message }); }
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserDto dto)
        {
            try { return Ok(await _svc.UpdateUserAsync(id, dto)); }
            catch (Exception ex) { return StatusCode(500, new { message = ex.Message }); }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            try { await _svc.DeleteUserAsync(id); return NoContent(); }
            catch (Exception ex) { return StatusCode(500, new { message = ex.Message }); }
        }

        [HttpPatch("{id:guid}/toggle-active")]
        public async Task<IActionResult> ToggleActive(Guid id)
        {
            try { await _svc.ToggleActiveAsync(id); return NoContent(); }
            catch (Exception ex) { return StatusCode(500, new { message = ex.Message }); }
        }

        [HttpPatch("{id:guid}/role")]
        public async Task<IActionResult> AssignRole(Guid id, [FromBody] AssignRoleDto dto)
        {
            try { await _svc.AssignRoleAsync(id, dto.Role); return NoContent(); }
            catch (Exception ex) { return StatusCode(500, new { message = ex.Message }); }
        }

        [HttpPost("{id:guid}/generate-credentials")]
        public async Task<IActionResult> GenerateCredentials(Guid id, [FromBody] GenerateCredentialsDto dto)
        {
            try { return Ok(await _svc.GenerateCredentialsAsync(id, dto)); }
            catch (Exception ex) { return StatusCode(500, new { message = ex.Message }); }
        }


        [HttpPost("{id:guid}/resend-credentials")]
        public async Task<IActionResult> ResendCredentials(Guid id)
        {
            try { await _svc.ResendCredentialsAsync(id); return NoContent(); }
            catch (Exception ex) { return StatusCode(500, new { message = ex.Message }); }
        }
    }
}
