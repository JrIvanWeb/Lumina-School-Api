using LuminiSchool.Business.Services.Contract;
using LuminiSchool.Domain.Model.Observer.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LuminiSchool.Presentation.Controllers
{
    [ApiController]
    [Route("api/observers")]
    [Authorize]
    public class ObserversController : ControllerBase
    {
        private readonly IObserverService _svc;
        public ObserversController(IObserverService svc) { _svc = svc; }
        [HttpGet("student/{sid:guid}")] public async Task<IActionResult> GetByStudent(Guid sid) => Ok(await _svc.GetByStudentAsync(sid));
        [HttpPost][Authorize(Roles = "SuperAdmin,Admin,Rector,Teacher")] public async Task<IActionResult> Create([FromBody] CreateObserverDto dto) => Ok(await _svc.CreateAsync(dto));
        [HttpDelete("{id:guid}")][Authorize(Roles = "SuperAdmin,Admin,Rector")] public async Task<IActionResult> Delete(Guid id) { await _svc.DeleteAsync(id); return NoContent(); }
    }
}
