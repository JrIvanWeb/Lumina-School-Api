using LuminiSchool.Business.Services.Contract;
using LuminiSchool.Domain.Entities.Achievement;
using LuminiSchool.Domain.Model.Achievement.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LuminiSchool.Presentation.Controllers
{
    /// <summary>
    /// Banco de Logros — CRUD completo con filtros por Período, Grado,
    /// Asignatura y Nivel de desempeño (sistema colombiano MEN).
    /// </summary>
    [ApiController]
    [Route("api/achievements")]
    [Authorize]
    public class AchievementsController : ControllerBase
    {
        private readonly IAchievementService _svc;

        public AchievementsController(IAchievementService svc) => _svc = svc;

        // ── GET /api/achievements ─────────────────────────────────────────────
        // Acepta query strings opcionales: periodId, gradeId, subjectId, performance
        // Ejemplo: GET /api/achievements?periodId=...&gradeId=...&performance=Superior
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] Guid?   periodId    = null,
            [FromQuery] Guid?   gradeId     = null,
            [FromQuery] Guid?   subjectId   = null,
            [FromQuery] string? performance = null)
        {
            // Parsear el enum de desempeño si viene como string
            PerformanceLevel? perfLevel = null;
            if (!string.IsNullOrWhiteSpace(performance) &&
                Enum.TryParse<PerformanceLevel>(performance, ignoreCase: true, out var parsed))
            {
                perfLevel = parsed;
            }

            var result = await _svc.GetFilteredAsync(periodId, gradeId, subjectId, perfLevel);
            return Ok(result);
        }

        // ── GET /api/achievements/{id} ────────────────────────────────────────
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var dto = await _svc.GetByIdAsync(id);
            return Ok(dto);
        }

        // ── POST /api/achievements ────────────────────────────────────────────
        [HttpPost]
        [Authorize(Roles = "SuperAdmin,Admin,Rector,Teacher")]
        public async Task<IActionResult> Create([FromBody] CreateAchievementDto dto)
        {
            var created = await _svc.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // ── PUT /api/achievements/{id} ────────────────────────────────────────
        [HttpPut("{id:guid}")]
        [Authorize(Roles = "SuperAdmin,Admin,Rector,Teacher")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAchievementDto dto)
        {
            var updated = await _svc.UpdateAsync(id, dto);
            return Ok(updated);
        }

        // ── DELETE /api/achievements/{id} ─────────────────────────────────────
        // Eliminación lógica (IsActive = false). Solo roles administrativos.
        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "SuperAdmin,Admin,Rector")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _svc.DeleteAsync(id);
            return NoContent();
        }
    }
}
