using AutoMapper;
using LuminiSchool.Business.Exceptions;
using LuminiSchool.Business.Services.Contract;
using LuminiSchool.Domain.Entities.Teacher;
using LuminiSchool.Domain.Model.Teacher.DTOs;
using LuminiSchool.Infrastructure.Repositories.Contract;

namespace LuminiSchool.Business.Services.Implementation
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _r; private readonly IMapper _m;
        public TeacherService(ITeacherRepository r, IMapper m) { _r = r; _m = m; }
        public async Task<IEnumerable<TeacherDto>> GetAllAsync() => _m.Map<IEnumerable<TeacherDto>>(await _r.GetAllAsync());
        public async Task<TeacherDto> GetByIdAsync(Guid id) => _m.Map<TeacherDto>(await _r.GetByIdAsync(id) ?? throw new NotFoundException($"Docente {id} no encontrado."));
        public async Task<TeacherDto> CreateAsync(CreateTeacherDto dto) { if (await _r.GetByDocumentAsync(dto.DocumentNumber) != null) throw new BusinessException($"Ya existe un docente con documento {dto.DocumentNumber}."); var e = _m.Map<TeacherEntity>(dto); e.Id = Guid.NewGuid(); return _m.Map<TeacherDto>(await _r.AddAsync(e)); }
        public async Task<TeacherDto> UpdateAsync(Guid id, UpdateTeacherDto dto) { var e = await _r.GetByIdAsync(id) ?? throw new NotFoundException($"Docente {id} no encontrado."); _m.Map(dto, e); e.UpdatedAt = DateTime.UtcNow; await _r.UpdateAsync(e); return _m.Map<TeacherDto>(e); }
        public async Task DeleteAsync(Guid id) { if (!await _r.ExistsAsync(id)) throw new NotFoundException($"Docente {id} no encontrado."); await _r.DeleteAsync(id); }
        public async Task AssignSubjectAsync(Guid tid, Guid sid) => await _r.AssignSubjectAsync(tid, sid);
        public async Task RemoveSubjectAsync(Guid tid, Guid sid) => await _r.RemoveSubjectAsync(tid, sid);
    }
}
