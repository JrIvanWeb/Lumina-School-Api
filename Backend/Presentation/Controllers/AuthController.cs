using LuminiSchool.Business.Services.Contract;
using LuminiSchool.Domain.Model.User.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LuminiSchool.Presentation.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _svc;

        public AuthController (IAuthService svc)
        {
            _svc = svc;
        }
        [HttpPost("login")][AllowAnonymous] public async Task<IActionResult> Login([FromBody] LoginDto dto) => Ok(await _svc.LoginAsync(dto));
        [HttpPost("register")][Authorize(Roles = "SuperAdmin,Admin")] public async Task<IActionResult> Register([FromBody] RegisterUserDto dto) => Ok(await _svc.RegisterAsync(dto));
        [HttpPost("change-password")][Authorize] public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto) { await _svc.ChangePasswordAsync(Uid(), dto); return NoContent(); }
        private Guid Uid() => Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
    }
}
