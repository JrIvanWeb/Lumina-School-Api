using LuminiSchool.Business.Services.Contract;
using LuminiSchool.Domain.Entities.Certificate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LuminiSchool.Presentation.Controllers
{
    [ApiController]
    [Route("api/certificates")]
    [Authorize]
    public class CertificatesController : ControllerBase
    {
        private readonly ICertificateService _svc;
        public CertificatesController(ICertificateService svc) { _svc = svc; }
        [HttpGet("student/{sid:guid}")] public async Task<IActionResult> GetByStudent(Guid sid) => Ok(await _svc.GetByStudentAsync(sid));
        [HttpGet("pdf/student/{sid:guid}/type/{type}")] public async Task<IActionResult> Pdf(Guid sid, CertificateType type) { var f = await _svc.GeneratePdfAsync(sid, type); return File(f, "application/pdf", $"certificado_{type}_{sid}.pdf"); }
    }
}
