using LuminiSchool.Business.Services.Contract;
using LuminiSchool.Domain.Model.Schedule.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LuminiSchool.Presentation.Controllers
{
    [ApiController]
    [Route("api/schedules")]
    [Authorize]
    public class SchedulesController : ControllerBase
    {
        private readonly IScheduleService _svc;
        public SchedulesController(IScheduleService svc) { _svc = svc; }
        [HttpGet("grade/{gid:guid}")] public async Task<IActionResult> GetByGrade(Guid gid) => Ok(await _svc.GetByGradeAsync(gid));
        [HttpGet("teacher/{tid:guid}")] public async Task<IActionResult> GetByTeacher(Guid tid) => Ok(await _svc.GetByTeacherAsync(tid));
        [HttpPost][Authorize(Roles = "SuperAdmin,Admin,Rector")] public async Task<IActionResult> Create([FromBody] CreateScheduleDto dto) => Ok(await _svc.CreateAsync(dto));
        [HttpDelete("{id:guid}")][Authorize(Roles = "SuperAdmin,Admin,Rector")] public async Task<IActionResult> Delete(Guid id) { await _svc.DeleteAsync(id); return NoContent(); }
    }
}
