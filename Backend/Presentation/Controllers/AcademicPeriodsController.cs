using LuminiSchool.Business.Services.Contract;
using LuminiSchool.Domain.Model.AcademicPeriod.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LuminiSchool.Presentation.Controllers
{
    [ApiController]
    [Route("api/academic-periods")]
    [Authorize]
    public class AcademicPeriodsController : ControllerBase
    {
        private readonly IAcademicPeriodService _svc;
        public AcademicPeriodsController(IAcademicPeriodService svc) { _svc = svc; }
        [HttpGet] public async Task<IActionResult> GetAll() => Ok(await _svc.GetAllAsync());
        [HttpGet("active")] public async Task<IActionResult> GetActive() => Ok(await _svc.GetActiveAsync());
        [HttpGet("year/{year:int}")] public async Task<IActionResult> GetByYear(int year) => Ok(await _svc.GetByYearAsync(year));
        [HttpPost][Authorize(Roles = "SuperAdmin,Admin,Rector")] public async Task<IActionResult> Create([FromBody] CreateAcademicPeriodDto dto) => Ok(await _svc.CreateAsync(dto));
        [HttpDelete("{id:guid}")][Authorize(Roles = "SuperAdmin,Admin")] public async Task<IActionResult> Delete(Guid id) { await _svc.DeleteAsync(id); return NoContent(); }
    }
}
