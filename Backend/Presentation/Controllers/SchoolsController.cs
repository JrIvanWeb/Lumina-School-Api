
using LuminiSchool.Business.Services.Contract;
using LuminiSchool.Domain.Model.School.DTOs;
using LuminiSchool.Infrastructure.Repositories.Contract;
using Microsoft.AspNetCore.Mvc;

namespace LuminiSchool.Presentation.Controllers
{
    [ApiController]
    [Route("api/schools")]
    public class SchoolsController : ControllerBase
    {
        private readonly ISchoolService _svc;

        public SchoolsController(ISchoolService svc) => _svc = svc;

        /// <summary>Retorna todos los colegios registrados.</summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _svc.GetAllAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error obteniendo colegios", detail = ex.Message });
            }
        }

        /// <summary>Retorna un colegio por su ID.</summary>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var result = await _svc.GetByIdAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error obteniendo colegio", detail = ex.Message });
            }
        }

        /// <summary>Crea un nuevo colegio.</summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSchoolDto dto)
        {
            try
            {
                var created = await _svc.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error creando colegio", detail = ex.Message });
            }
        }

        /// <summary>Elimina un colegio por su ID.</summary>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _svc.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error eliminando colegio", detail = ex.Message });
            }
        }
    }
}
