using LuminiSchool.Business.Services.Contract;
using LuminiSchool.Domain.Model.Enrollment.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LuminiSchool.Presentation.Controllers
{
    [ApiController]
    [Route("api/enrollments")]
    [Authorize]
    public class EnrollmentsController : ControllerBase
    {
        private readonly IEnrollmentService _svc;
        public EnrollmentsController(IEnrollmentService svc) => _svc = svc;

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _svc.GetAllAsync());

        [HttpGet("student/{sid:guid}")]
        public async Task<IActionResult> GetByStudent(Guid sid) =>
            Ok(await _svc.GetByStudentAsync(sid));

        /// <summary>Matrícula simple para estudiante ya registrado.</summary>
        [HttpPost]
        [Authorize(Roles = "SuperAdmin,Admin,Rector")]
        public async Task<IActionResult> Create([FromBody] CreateEnrollmentDto dto) =>
            Ok(await _svc.CreateAsync(dto));

        /// <summary>
        /// Ficha completa de matrícula:
        /// crea estudiante, registra padres (opcional) y acudiente (obligatorio)
        /// en un solo request.
        /// </summary>
        [HttpPost("ficha")]
        [Authorize(Roles = "SuperAdmin,Admin,Rector")]
        public async Task<IActionResult> CreateFicha([FromBody] CreateFichaMatriculaDto dto) =>
            Ok(await _svc.CreateFichaAsync(dto));

        [HttpPatch("{id:guid}/withdraw")]
        [Authorize(Roles = "SuperAdmin,Admin,Rector")]
        public async Task<IActionResult> Withdraw(Guid id)
        {
            await _svc.WithdrawAsync(id);
            return NoContent();
        }
    }
}
