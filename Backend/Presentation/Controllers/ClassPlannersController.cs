using LuminiSchool.Business.Services.Contract;
using LuminiSchool.Domain.Model.ClassPlanner.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LuminiSchool.Presentation.Controllers
{
    [ApiController]
    [Route("api/class-planners")]
    [Authorize]
    public class ClassPlannersController : ControllerBase
    {
        private readonly IClassPlannerService _svc;
        public ClassPlannersController(IClassPlannerService svc) { _svc = svc; }
        [HttpGet] public async Task<IActionResult> GetAll() => Ok(await _svc.GetAllAsync());
        [HttpGet("{id:guid}")] public async Task<IActionResult> GetById(Guid id) => Ok(await _svc.GetByIdAsync(id));
        [HttpGet("teacher/{tid:guid}")] public async Task<IActionResult> GetByTeacher(Guid tid) => Ok(await _svc.GetByTeacherAsync(tid));
        [HttpPost][Authorize(Roles = "SuperAdmin,Admin,Rector,Teacher")] public async Task<IActionResult> Create([FromBody] CreateClassPlannerDto dto) { var r = await _svc.CreateAsync(dto); return CreatedAtAction(nameof(GetById), new { id = r.Id }, r); }
        [HttpPut("{id:guid}")][Authorize(Roles = "SuperAdmin,Admin,Rector,Teacher")] public async Task<IActionResult> Update(Guid id, [FromBody] UpdateClassPlannerDto dto) => Ok(await _svc.UpdateAsync(id, dto));
        [HttpDelete("{id:guid}")][Authorize(Roles = "SuperAdmin,Admin,Rector")] public async Task<IActionResult> Delete(Guid id) { await _svc.DeleteAsync(id); return NoContent(); }
    }
}
