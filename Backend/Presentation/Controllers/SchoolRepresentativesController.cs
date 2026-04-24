using LuminiSchool.Business.Services.Contract;
using LuminiSchool.Domain.Model.SchoolRepresentative.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LuminiSchool.Presentation.Controllers
{
    [ApiController]
    [Route("api/school-representatives")]
    [Authorize]
    public class SchoolRepresentativesController : ControllerBase
    {
        private readonly ISchoolRepresentativeService _svc;
        public SchoolRepresentativesController(ISchoolRepresentativeService svc) { _svc = svc; }
        [HttpGet("active")] public async Task<IActionResult> GetActive() => Ok(await _svc.GetActiveAsync());
        [HttpGet("year/{year:int}")] public async Task<IActionResult> GetByYear(int year) => Ok(await _svc.GetByYearAsync(year));
        [HttpPost][Authorize(Roles = "SuperAdmin,Admin,Rector")] public async Task<IActionResult> Create([FromBody] CreateSchoolRepresentativeDto dto) => Ok(await _svc.CreateAsync(dto));
        [HttpDelete("{id:guid}")][Authorize(Roles = "SuperAdmin,Admin,Rector")] public async Task<IActionResult> Delete(Guid id) { await _svc.DeleteAsync(id); return NoContent(); }
    }
}
