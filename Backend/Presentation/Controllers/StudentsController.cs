using LuminiSchool.Business.Services.Contract;
using LuminiSchool.Domain.Model.Student.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LuminiSchool.Presentation.Controllers
{
   
    [Route("api/students")]
    [ApiController]
    [Authorize]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _svc;
        public StudentsController(IStudentService svc)
        { 
            _svc = svc; 
        }
        [HttpGet] 
        public async Task<IActionResult> GetAll() => Ok(await _svc.GetAllAsync());

        [HttpGet("{id:guid}")] 
        public async Task<IActionResult> GetById(Guid id) => Ok(await _svc.GetByIdAsync(id));
        [HttpGet("grade/{gradeId:guid}")] 
        public async Task<IActionResult> GetByGrade(Guid gradeId) => Ok(await _svc.GetByGradeAsync(gradeId));
        [HttpGet("birthdays/today")] 
        public async Task<IActionResult> Birthdays() => Ok(await _svc.GetBirthdaysTodayAsync());

        [Authorize(Roles = "SuperAdmin,Admin,Rector")]
        [HttpPost] 
        public async Task<IActionResult> Create([FromBody] CreateStudentDto dto) 
        { 
            var r = await _svc.CreateAsync(dto); 
            return CreatedAtAction(nameof(GetById), new { id = r.Id }, r); 
        }

        [Authorize(Roles = "SuperAdmin,Admin,Rector")]
        [HttpPut("{id:guid}")] 
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateStudentDto dto) => Ok(await _svc.UpdateAsync(id, dto));
        
        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id) 
        { 
            await _svc.DeleteAsync(id); return NoContent(); 
        }
    }
}
