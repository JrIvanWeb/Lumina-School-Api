using LuminiSchool.Business.Services.Contract;
using LuminiSchool.Domain.Model.GradeSubjectTeacher.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LuminiSchool.Presentation.Controllers
{
    [ApiController]
    [Route("api/grade-subject-teachers")]
    [Authorize]
    public class GradeSubjectTeachersController : ControllerBase
    {
        private readonly IGradeSubjectTeacherService _svc;

        public GradeSubjectTeachersController(IGradeSubjectTeacherService svc)
        {
            _svc = svc;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _svc.GetAllAsync());

        [HttpGet("by-grade/{gradeId:guid}")]
        public async Task<IActionResult> GetByGrade(Guid gradeId) =>
            Ok(await _svc.GetByGradeAsync(gradeId));

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id) =>
            Ok(await _svc.GetByIdAsync(id));

        [Authorize(Roles = "SuperAdmin,Admin,Rector")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateGradeSubjectTeacherDto dto)
        {
            var r = await _svc.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = r.Id }, r);
        }

        [Authorize(Roles = "SuperAdmin,Admin,Rector")]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateGradeSubjectTeacherDto dto) =>
            Ok(await _svc.UpdateAsync(id, dto));

        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _svc.DeleteAsync(id);
            return NoContent();
        }
    }
}
