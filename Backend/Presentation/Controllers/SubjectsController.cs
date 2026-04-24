using LuminiSchool.Business.Services.Contract;
using LuminiSchool.Domain.Model.Subject.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LuminiSchool.Presentation.Controllers
{
    [ApiController]
    [Route("api/subjects")]
    [Authorize]
    public class SubjectsController : ControllerBase
    {
        private readonly ISubjectService _svc;
        public SubjectsController(ISubjectService svc) 
        { 
            _svc = svc; 
        }
        [HttpGet] 
        public async Task<IActionResult> GetAll() => Ok(await _svc.GetAllAsync());
        [HttpGet("{id:guid}")] 
        public async Task<IActionResult> GetById(Guid id) => Ok(await _svc.GetByIdAsync(id));
        [Authorize(Roles = "SuperAdmin,Admin,Rector")]
        [HttpPost] 
        public async Task<IActionResult> Create([FromBody] CreateSubjectDto dto) { var r = await _svc.CreateAsync(dto); return CreatedAtAction(nameof(GetById), new { id = r.Id }, r); }
        
        [Authorize(Roles = "SuperAdmin,Admin,Rector")]
        [HttpPut("{id:guid}")] 
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateSubjectDto dto) => Ok(await _svc.UpdateAsync(id, dto));
        
        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id) 
        {
            await _svc.DeleteAsync(id); return NoContent();
        }
    }
}
