using AutoMapper;
using LuminiSchool.Business.Exceptions;
using LuminiSchool.Business.Services.Contract;
using LuminiSchool.Domain.Entities.Guardian;
using LuminiSchool.Domain.Model.Guardian.DTOs;
using LuminiSchool.Infrastructure.Repositories.Contract;

namespace LuminiSchool.Business.Services.Implementation
{
    public class GuardianService : IGuardianService
    {
        private readonly IGuardianRepository _r; private readonly IMapper _m;
        public GuardianService(IGuardianRepository r, IMapper m) { _r = r; _m = m; }
        public async Task<IEnumerable<GuardianDto>> GetAllAsync() => _m.Map<IEnumerable<GuardianDto>>(await _r.GetAllAsync());
        public async Task<GuardianDto> GetByIdAsync(Guid id) => _m.Map<GuardianDto>(await _r.GetByIdAsync(id) ?? throw new NotFoundException($"Acudiente {id} no encontrado."));
        public async Task<GuardianDto> CreateAsync(CreateGuardianDto dto) { var e = _m.Map<GuardianEntity>(dto); e.Id = Guid.NewGuid(); var c = await _r.AddAsync(e); foreach (var sid in dto.StudentIds) await _r.AssignStudentAsync(c.Id, sid); return _m.Map<GuardianDto>(c); }
        public async Task<GuardianDto> UpdateAsync(Guid id, UpdateGuardianDto dto) { var e = await _r.GetByIdAsync(id) ?? throw new NotFoundException($"Acudiente {id} no encontrado."); _m.Map(dto, e); await _r.UpdateAsync(e); return _m.Map<GuardianDto>(e); }
        public async Task DeleteAsync(Guid id) { if (!await _r.ExistsAsync(id)) throw new NotFoundException($"Acudiente {id} no encontrado."); await _r.DeleteAsync(id); }
        public async Task<IEnumerable<GuardianDto>> GetByStudentAsync(Guid sid) => _m.Map<IEnumerable<GuardianDto>>(await _r.GetByStudentAsync(sid));
        public async Task AssignStudentAsync(Guid gid, Guid sid) => await _r.AssignStudentAsync(gid, sid);
    }
}
