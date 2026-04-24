using LuminiSchool.Business.Services.Contract;
using LuminiSchool.Domain.Model.IcfesSimulator.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LuminiSchool.Presentation.Controllers
{
    [ApiController]
    [Route("api/icfes-simulators")]
    [Authorize]
    public class IcfesSimulatorsController : ControllerBase
    {
        private readonly IIcfesSimulatorService _svc;
        public IcfesSimulatorsController(IIcfesSimulatorService svc) { _svc = svc; }
        [HttpGet] public async Task<IActionResult> GetAll() => Ok(await _svc.GetAllAsync());
        [HttpGet("{id:guid}")] public async Task<IActionResult> GetById(Guid id) => Ok(await _svc.GetByIdAsync(id));
        [HttpGet("{id:guid}/results")] public async Task<IActionResult> GetResults(Guid id) => Ok(await _svc.GetResultsBySimulatorAsync(id));
        [HttpGet("student/{sid:guid}/results")] public async Task<IActionResult> GetResultsByStudent(Guid sid) => Ok(await _svc.GetResultsByStudentAsync(sid));
        [HttpPost][Authorize(Roles = "SuperAdmin,Admin,Rector")] public async Task<IActionResult> Create([FromBody] CreateIcfesSimulatorDto dto) { var r = await _svc.CreateAsync(dto, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value)); return CreatedAtAction(nameof(GetById), new { id = r.Id }, r); }
        [HttpPatch("{id:guid}/activate")][Authorize(Roles = "SuperAdmin,Admin,Rector")] public async Task<IActionResult> Activate(Guid id) { await _svc.ActivateAsync(id); return NoContent(); }
        [HttpPatch("{id:guid}/complete")][Authorize(Roles = "SuperAdmin,Admin,Rector")] public async Task<IActionResult> Complete(Guid id) { await _svc.CompleteAsync(id); return NoContent(); }
        [HttpPost("results")] public async Task<IActionResult> RegisterResult([FromBody] CreateIcfesResultDto dto) => Ok(await _svc.RegisterResultAsync(dto));
    }
}
