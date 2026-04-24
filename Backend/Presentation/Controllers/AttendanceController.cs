using LuminiSchool.Business.Services.Contract;
using LuminiSchool.Domain.Model.Attendance.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LuminiSchool.Presentation.Controllers
{
    [ApiController]
    [Route("api/attendance")]
    [Authorize]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _svc;
        public AttendanceController(IAttendanceService svc) { _svc = svc; }
        [HttpGet("student/{sid:guid}")] public async Task<IActionResult> GetByStudent(Guid sid) => Ok(await _svc.GetByStudentAsync(sid));
        [HttpGet("grade/{gid:guid}/date/{date}")] public async Task<IActionResult> GetByGradeDate(Guid gid, DateTime date) => Ok(await _svc.GetByDateAndGradeAsync(date, gid));
        [HttpGet("report/student/{sid:guid}")] public async Task<IActionResult> GetReport(Guid sid, [FromQuery] DateTime from, [FromQuery] DateTime to) => Ok(await _svc.GetReportAsync(sid, from, to));
        [HttpPost][Authorize(Roles = "SuperAdmin,Admin,Rector,Teacher")] public async Task<IActionResult> Register([FromBody] CreateAttendanceDto dto) => Ok(await _svc.RegisterAsync(dto));
    }
}
