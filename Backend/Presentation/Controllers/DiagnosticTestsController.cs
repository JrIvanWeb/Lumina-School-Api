using LuminiSchool.Business.Services.Contract;
using LuminiSchool.Domain.Model.DiagnosticTest.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LuminiSchool.Presentation.Controllers
{
    [ApiController]
    [Route("api/diagnostic-tests")]
    [Authorize]
    public class DiagnosticTestsController : ControllerBase
    {
        private readonly IDiagnosticTestService _svc;
        public DiagnosticTestsController(IDiagnosticTestService svc) { _svc = svc; }
        [HttpGet] public async Task<IActionResult> GetAll() => Ok(await _svc.GetAllAsync());
        [HttpGet("{id:guid}")] public async Task<IActionResult> GetById(Guid id) => Ok(await _svc.GetByIdAsync(id));
        [HttpGet("teacher/{tid:guid}")] public async Task<IActionResult> GetByTeacher(Guid tid) => Ok(await _svc.GetByTeacherAsync(tid));
        [HttpGet("{id:guid}/results")] public async Task<IActionResult> GetResults(Guid id) => Ok(await _svc.GetResultsAsync(id));
        [HttpPost][Authorize(Roles = "SuperAdmin,Admin,Rector,Teacher")] public async Task<IActionResult> Create([FromBody] CreateDiagnosticTestDto dto) { var r = await _svc.CreateAsync(dto); return CreatedAtAction(nameof(GetById), new { id = r.Id }, r); }
        [HttpPost("results")] public async Task<IActionResult> RegisterResult([FromBody] CreateDiagnosticResultDto dto) => Ok(await _svc.RegisterResultAsync(dto));
        [HttpDelete("{id:guid}")][Authorize(Roles = "SuperAdmin,Admin,Rector")] public async Task<IActionResult> Delete(Guid id) { await _svc.DeleteAsync(id); return NoContent(); }
    }
}
