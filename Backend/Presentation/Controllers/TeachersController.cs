using LuminiSchool.Business.Services.Contract;
using LuminiSchool.Domain.Model.Teacher.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LuminiSchool.Presentation.Controllers
{
    [ApiController]
    [Route("api/teachers")]
    public class TeachersController : ControllerBase
    {
        private readonly ITeacherService _svc;
        public TeachersController(ITeacherService svc) 
        { 
            _svc = svc; 
        }

        [HttpGet] 
        public async Task<IActionResult> GetAll() => Ok(await _svc.GetAllAsync());

        [HttpGet("{id:guid}")] 
        public async Task<IActionResult> GetById(Guid id) => Ok(await _svc.GetByIdAsync(id));

        [Authorize(Roles = "SuperAdmin,Admin,Rector")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTeacherDto dto) 
        { 
            var r = await _svc.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = r.Id }, r); 
        }

        [Authorize(Roles = "SuperAdmin,Admin,Rector")]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTeacherDto dto) => Ok(await _svc.UpdateAsync(id, dto));

        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpDelete("{id:guid}")] 
        public async Task<IActionResult> Delete(Guid id) 
        { 
            await _svc.DeleteAsync(id); return NoContent(); 
        }

        [Authorize(Roles = "SuperAdmin,Admin,Rector")]
        [HttpPost("{tid:guid}/subjects/{sid:guid}")] 
        public async Task<IActionResult> AssignSubject(Guid tid, Guid sid) 
        { 
            await _svc.AssignSubjectAsync(tid, sid); return NoContent(); 
        }

        [Authorize(Roles = "SuperAdmin,Admin,Rector")]
        [HttpDelete("{tid:guid}/subjects/{sid:guid}")]
        public async Task<IActionResult> RemoveSubject(Guid tid, Guid sid) 
        { 
            await _svc.RemoveSubjectAsync(tid, sid); return NoContent(); 
        }
    }
}
