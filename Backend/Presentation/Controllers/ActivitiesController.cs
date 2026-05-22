using LuminiSchool.Business.Services.Contract;
using LuminiSchool.Domain.Model.Activity.DTOs;
using LuminiSchool.Infrastructure.Repositories.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LuminiSchool.Presentation.Controllers
{
    [ApiController]
    [Route("api/activities")]
    [Authorize]
    public class ActivitiesController : ControllerBase
    {
        private readonly IActivityService   _svc;
        private readonly ITeacherRepository _teachers;

        public ActivitiesController(IActivityService svc, ITeacherRepository teachers)
        {
            _svc      = svc;
            _teachers = teachers;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _svc.GetAllAsync());

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
            => Ok(await _svc.GetByIdAsync(id));

        [HttpGet("teacher/{tid:guid}")]
        public async Task<IActionResult> GetByTeacher(Guid tid)
            => Ok(await _svc.GetByTeacherAsync(tid));

        [HttpGet("{aid:guid}/submissions")]
        public async Task<IActionResult> GetSubmissions(Guid aid)
            => Ok(await _svc.GetSubmissionsAsync(aid));

        [HttpPost]
        [Authorize(Roles = "SuperAdmin,Admin,Rector,Teacher")]
        public async Task<IActionResult> Create([FromBody] CreateActivityDto dto)
        {
            var userId  = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var teacher = await _teachers.GetByUserIdAsync(userId);

            // Si el usuario logueado tiene un TeacherEntity lo asociamos;
            // si es SuperAdmin/Admin/Rector sin perfil de docente, TeacherId queda null
            // (la columna ahora acepta NULL en la BD).
            dto.TeacherId = teacher?.Id;   // Guid? — ya no asignamos Guid.Empty

            var result = await _svc.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPost("submit")]
        public async Task<IActionResult> Submit([FromBody] SubmitActivityDto dto)
            => Ok(await _svc.SubmitAsync(dto));

        [HttpPost("grade")]
        public async Task<IActionResult> Grade([FromBody] GradeSubmissionDto dto)
            => Ok(await _svc.GradeSubmissionAsync(dto));

        [HttpPut("{id:guid}")]
        [Authorize(Roles = "SuperAdmin,Admin,Rector,Teacher")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CreateActivityDto dto)
        {
            var result = await _svc.UpdateAsync(id, dto);
            return Ok(result);
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "SuperAdmin,Admin,Rector")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _svc.DeleteAsync(id);
            return NoContent();
        }
    }
}
