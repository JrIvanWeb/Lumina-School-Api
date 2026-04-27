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
        private readonly IAuthService _auth;
        public AuthController(IAuthService auth) => _auth = auth;

        /// <summary>Iniciar sesión. Devuelve JWT + refresh token.</summary>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDto dto) =>
            Ok(await _auth.LoginAsync(dto));

        /// <summary>Renovar JWT usando el refresh token.</summary>
        [HttpPost("refresh-token")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDto dto) =>
            Ok(await _auth.RefreshTokenAsync(dto));

        /// <summary>
        /// Cambio de contraseña obligatorio en el primer inicio de sesión.
        /// El frontend debe llamar este endpoint cuando RequiresPasswordChange = true.
        /// </summary>
        [HttpPost("force-change-password")]
        [Authorize]
        public async Task<IActionResult> ForceChangePassword([FromBody] ForceChangePasswordDto dto)
        {
            await _auth.ForceChangePasswordAsync(UserId(), dto);
            return NoContent();
        }

        /// <summary>Cambio voluntario de contraseña (usuario autenticado).</summary>
        [HttpPost("change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
        {
            await _auth.ChangePasswordAsync(UserId(), dto);
            return NoContent();
        }

        private Guid UserId() =>
            Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    }
}
