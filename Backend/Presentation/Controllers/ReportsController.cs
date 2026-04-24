using LuminiSchool.Business.Services.Contract;
using LuminiSchool.Domain.Model.Report.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LuminiSchool.Presentation.Controllers
{
    [ApiController]
    [Route("api/reports")]
    [Authorize(Roles = "SuperAdmin,Admin,Rector")]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _svc;
        public ReportsController(IReportService svc) { _svc = svc; }
        [HttpGet] public async Task<IActionResult> GetAll() => Ok(await _svc.GetAllAsync());
        [HttpPost] public async Task<IActionResult> Generate([FromBody] CreateReportDto dto) => Ok(await _svc.GenerateAsync(dto, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value)));
    }
}
