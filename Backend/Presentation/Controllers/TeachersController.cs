using LuminiSchool.Business.Services.Contract;
using LuminiSchool.Domain.Model.Teacher.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LuminiSchool.Presentation.Controllers
{
    [ApiController]
    [Route("api/teachers")]
    public class TeachersController : ControllerBase
    {
        private readonly ITeacherService _svc;
        public TeachersController(ITeacherService svc) => _svc = svc;

        /// <summary>Listar todos los docentes.</summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _svc.GetAllAsync());

        /// <summary>Obtener un docente por Id.</summary>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
            => Ok(await _svc.GetByIdAsync(id));

        /// <summary>
        /// Crear un docente. Crea también su ApplicationUser con rol Teacher
        /// y envía contraseña temporal por correo.
        /// </summary>
        [Authorize(Roles = "SuperAdmin,Admin,Rector")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTeacherDto dto)
        {
            var result = await _svc.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        /// <summary>Actualizar perfil de un docente.</summary>
        [Authorize(Roles = "SuperAdmin,Admin,Rector")]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTeacherDto dto)
            => Ok(await _svc.UpdateAsync(id, dto));

        /// <summary>Activar / desactivar un docente (y su usuario).</summary>
        [Authorize(Roles = "SuperAdmin,Admin,Rector")]
        [HttpPatch("{id:guid}/toggle-active")]
        public async Task<IActionResult> ToggleActive(Guid id)
        {
            await _svc.ToggleActiveAsync(id);
            return NoContent();
        }

        /// <summary>Eliminar un docente (soft-delete del usuario).</summary>
        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _svc.DeleteAsync(id);
            return NoContent();
        }

        /// <summary>Asignar una materia a un docente.</summary>
        [Authorize(Roles = "SuperAdmin,Admin,Rector")]
        [HttpPost("{tid:guid}/subjects/{sid:guid}")]
        public async Task<IActionResult> AssignSubject(Guid tid, Guid sid)
        {
            await _svc.AssignSubjectAsync(tid, sid);
            return NoContent();
        }

        /// <summary>Quitar una materia de un docente.</summary>
        [Authorize(Roles = "SuperAdmin,Admin,Rector")]
        [HttpDelete("{tid:guid}/subjects/{sid:guid}")]
        public async Task<IActionResult> RemoveSubject(Guid tid, Guid sid)
        {
            await _svc.RemoveSubjectAsync(tid, sid);
            return NoContent();
        }
    }
}
