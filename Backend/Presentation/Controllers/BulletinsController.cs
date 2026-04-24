using LuminiSchool.Business.Services.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LuminiSchool.Presentation.Controllers
{
    [ApiController]
    [Route("api/bulletins")]
    [Authorize]
    public class BulletinsController : ControllerBase
    {
        private readonly IBulletinService _svc;
        public BulletinsController(IBulletinService svc) { _svc = svc; }
        [HttpGet("student/{sid:guid}")] public async Task<IActionResult> GetByStudent(Guid sid) => Ok(await _svc.GetByStudentAsync(sid));
        [HttpPost("generate/student/{sid:guid}/period/{pid:guid}")][Authorize(Roles = "SuperAdmin,Admin,Rector,Teacher")] public async Task<IActionResult> Generate(Guid sid, Guid pid) => Ok(await _svc.GenerateAsync(sid, pid));
        [HttpGet("pdf/student/{sid:guid}/period/{pid:guid}")] public async Task<IActionResult> Pdf(Guid sid, Guid pid) { var f = await _svc.ExportPdfAsync(sid, pid); return File(f, "application/pdf", $"boletin_{sid}.pdf"); }
    }
}
