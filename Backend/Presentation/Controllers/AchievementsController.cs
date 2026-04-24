using LuminiSchool.Business.Services.Contract;
using LuminiSchool.Domain.Model.Achievement.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LuminiSchool.Presentation.Controllers
{
    [ApiController]
    [Route("api/achievements")]
    [Authorize]
    public class AchievementsController : ControllerBase
    {
        private readonly IAchievementService _svc;
        public AchievementsController(IAchievementService svc) { _svc = svc; }
        [HttpGet] public async Task<IActionResult> GetAll() => Ok(await _svc.GetAllAsync());
        [HttpGet("subject/{sid:guid}")] public async Task<IActionResult> GetBySubject(Guid sid) => Ok(await _svc.GetBySubjectAsync(sid));
        [HttpPost][Authorize(Roles = "SuperAdmin,Admin,Rector,Teacher")] public async Task<IActionResult> Create([FromBody] CreateAchievementDto dto) => Ok(await _svc.CreateAsync(dto));
        [HttpDelete("{id:guid}")][Authorize(Roles = "SuperAdmin,Admin,Rector")] public async Task<IActionResult> Delete(Guid id) { await _svc.DeleteAsync(id); return NoContent(); }
    }
}
