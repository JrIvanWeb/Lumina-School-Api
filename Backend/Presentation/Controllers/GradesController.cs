using LuminiSchool.Business.Services.Contract;
using LuminiSchool.Domain.Model.Grade.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LuminiSchool.Presentation.Controllers
{
    [ApiController]
    [Route("api/grades")]
    [Authorize]
    public class GradesController : ControllerBase
    {
        private readonly IGradeService _svc;
        public GradesController(IGradeService svc)
        {
            _svc = svc;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _svc.GetAllAsync());
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id) => Ok(await _svc.GetByIdAsync(id));

        [Authorize(Roles = "SuperAdmin,Admin,Rector")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateGradeDto dto)
        {
            var r = await _svc.CreateAsync(dto); return CreatedAtAction(nameof(GetById), new { id = r.Id }, r);
        }
        [Authorize(Roles = "SuperAdmin,Admin,Rector")]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateGradeDto dto) => Ok(await _svc.UpdateAsync(id, dto));

        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _svc.DeleteAsync(id); return NoContent();
        }
        [HttpPost("{gid:guid}/students/{sid:guid}")]
        public async Task<IActionResult> AssignStudent(Guid gid, Guid sid) { await _svc.AssignStudentAsync(gid, sid); return NoContent(); }
        [HttpDelete("{gid:guid}/students/{sid:guid}")]
        public async Task<IActionResult> RemoveStudent(Guid gid, Guid sid) { await _svc.RemoveStudentAsync(gid, sid); return NoContent(); }
        [HttpPost("{gid:guid}/subjects/{sid:guid}")]
        public async Task<IActionResult> AssignSubject(Guid gid, Guid sid) { await _svc.AssignSubjectAsync(gid, sid); return NoContent(); }
        [HttpPost("{gid:guid}/teachers/{tid:guid}")]
        public async Task<IActionResult> AssignTeacher(Guid gid, Guid tid) { await _svc.AssignTeacherAsync(gid, tid); return NoContent(); }
    }
}
