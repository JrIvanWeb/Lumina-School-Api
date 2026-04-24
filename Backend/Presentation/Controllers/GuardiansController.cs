using LuminiSchool.Business.Services.Contract;
using LuminiSchool.Domain.Model.Guardian.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LuminiSchool.Presentation.Controllers
{
    [ApiController]
    [Route("api/guardians")]
    [Authorize]
    public class GuardiansController : ControllerBase
    {
        private readonly IGuardianService _svc;
        public GuardiansController(IGuardianService svc) 
        { 
            _svc = svc; 
        }
        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _svc.GetAllAsync());

        [HttpGet("{id:guid}")] 
        public async Task<IActionResult> GetById(Guid id) => Ok(await _svc.GetByIdAsync(id));

        [HttpGet("student/{sid:guid}")] 
        public async Task<IActionResult> GetByStudent(Guid sid) => Ok(await _svc.GetByStudentAsync(sid));

        [Authorize(Roles = "SuperAdmin,Admin,Rector")]
        [HttpPost] 
        public async Task<IActionResult> Create([FromBody] CreateGuardianDto dto) { var r = await _svc.CreateAsync(dto); return CreatedAtAction(nameof(GetById), new { id = r.Id }, r); }
       
        [Authorize(Roles = "SuperAdmin,Admin,Rector")]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateGuardianDto dto) => Ok(await _svc.UpdateAsync(id, dto));
       
        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpDelete("{id:guid}")] 
        public async Task<IActionResult> Delete(Guid id) { await _svc.DeleteAsync(id); return NoContent(); }
        [HttpPost("{gid:guid}/students/{sid:guid}")] 
        public async Task<IActionResult> AssignStudent(Guid gid, Guid sid) 
        { 
            await _svc.AssignStudentAsync(gid, sid); return NoContent();
        }
    }
}
