using LuminiSchool.Business.Services.Contract;
using LuminiSchool.Domain.Model.Activity.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LuminiSchool.Presentation.Controllers
{
    [ApiController]
    [Route("api/activities")]
    [Authorize]
    public class ActivitiesController : ControllerBase
    {
        private readonly IActivityService _svc;
        public ActivitiesController(IActivityService svc) { _svc = svc; }
        [HttpGet] public async Task<IActionResult> GetAll() => Ok(await _svc.GetAllAsync());
        [HttpGet("{id:guid}")] public async Task<IActionResult> GetById(Guid id) => Ok(await _svc.GetByIdAsync(id));
        [HttpGet("teacher/{tid:guid}")] public async Task<IActionResult> GetByTeacher(Guid tid) => Ok(await _svc.GetByTeacherAsync(tid));
        [HttpGet("{aid:guid}/submissions")] public async Task<IActionResult> GetSubmissions(Guid aid) => Ok(await _svc.GetSubmissionsAsync(aid));
        [HttpPost][Authorize(Roles = "SuperAdmin,Admin,Rector,Teacher")] public async Task<IActionResult> Create([FromBody] CreateActivityDto dto) { var r = await _svc.CreateAsync(dto); return CreatedAtAction(nameof(GetById), new { id = r.Id }, r); }
        [HttpPost("submit")] public async Task<IActionResult> Submit([FromBody] SubmitActivityDto dto) => Ok(await _svc.SubmitAsync(dto));
        [HttpPost("grade")] public async Task<IActionResult> Grade([FromBody] GradeSubmissionDto dto) => Ok(await _svc.GradeSubmissionAsync(dto));
        [HttpDelete("{id:guid}")][Authorize(Roles = "SuperAdmin,Admin,Rector")] public async Task<IActionResult> Delete(Guid id) { await _svc.DeleteAsync(id); return NoContent(); }
    }
}
