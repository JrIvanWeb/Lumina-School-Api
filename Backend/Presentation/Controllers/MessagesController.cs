using LuminiSchool.Business.Services.Contract;
using LuminiSchool.Domain.Model.Message.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LuminiSchool.Presentation.Controllers
{
    [ApiController]
    [Route("api/messages")]
    [Authorize]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService _svc;
        public MessagesController(IMessageService svc) { _svc = svc; }
        [HttpGet("inbox")] public async Task<IActionResult> Inbox() => Ok(await _svc.GetInboxAsync(Uid()));
        [HttpGet("sent")] public async Task<IActionResult> Sent() => Ok(await _svc.GetSentAsync(Uid()));
        [HttpPost] public async Task<IActionResult> Send([FromBody] CreateMessageDto dto) { await _svc.SendAsync(dto, Uid()); return NoContent(); }
        private Guid Uid() => Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
    }
}
