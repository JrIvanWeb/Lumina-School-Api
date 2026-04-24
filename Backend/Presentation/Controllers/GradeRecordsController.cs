using LuminiSchool.Business.Services.Contract;
using LuminiSchool.Domain.Model.GradeRecord.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LuminiSchool.Presentation.Controllers
{
    [ApiController]
    [Route("api/grade-records")]
    [Authorize]
    public class GradeRecordsController : ControllerBase
    {
        private readonly IGradeRecordService _svc;
        public GradeRecordsController(IGradeRecordService svc) { _svc = svc; }
        [HttpGet("student/{sid:guid}/period/{pid:guid}")] public async Task<IActionResult> GetByStudentPeriod(Guid sid, Guid pid) => Ok(await _svc.GetByStudentAndPeriodAsync(sid, pid));
        [HttpGet("subject/{sid:guid}/period/{pid:guid}")] public async Task<IActionResult> GetBySubjectPeriod(Guid sid, Guid pid) => Ok(await _svc.GetBySubjectAndPeriodAsync(sid, pid));
        [HttpGet("average/student/{sid:guid}/period/{pid:guid}")] public async Task<IActionResult> GetAverage(Guid sid, Guid pid) => Ok(await _svc.GetAverageAsync(sid, pid));
        [HttpPost][Authorize(Roles = "SuperAdmin,Admin,Rector,Teacher")] public async Task<IActionResult> Register([FromBody] CreateGradeRecordDto dto) => Ok(await _svc.RegisterAsync(dto));
        [HttpPut("{id:guid}")][Authorize(Roles = "SuperAdmin,Admin,Rector,Teacher")] public async Task<IActionResult> Update(Guid id, [FromBody] UpdateGradeRecordDto dto) => Ok(await _svc.UpdateAsync(id, dto));
    }
}
