using LuminiSchool.Business.Services.Contract;
using LuminiSchool.Domain.Model.Notification.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LuminiSchool.Presentation.Controllers
{
    [ApiController]
    [Route("api/notifications")]
    [Authorize]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService _svc;
        public NotificationsController(INotificationService svc) { _svc = svc; }
        [HttpGet("me")] public async Task<IActionResult> GetMine() => Ok(await _svc.GetByUserAsync(Uid()));
        [HttpGet("me/unread")] public async Task<IActionResult> UnreadCount() => Ok(await _svc.GetUnreadCountAsync(Uid()));
        [HttpPost][Authorize(Roles = "SuperAdmin,Admin,Rector,Teacher")] public async Task<IActionResult> Send([FromBody] CreateNotificationDto dto) { await _svc.SendAsync(dto); return NoContent(); }
        [HttpPatch("{id:guid}/read")] public async Task<IActionResult> MarkRead(Guid id) { await _svc.MarkAsReadAsync(id); return NoContent(); }
        [HttpPatch("me/read-all")] public async Task<IActionResult> MarkAllRead() { await _svc.MarkAllAsReadAsync(Uid()); return NoContent(); }
        private Guid Uid() => Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
    }
}
