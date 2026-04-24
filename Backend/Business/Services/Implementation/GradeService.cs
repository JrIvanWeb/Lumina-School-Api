using AutoMapper;
using LuminiSchool.Business.Exceptions;
using LuminiSchool.Business.Services.Contract;
using LuminiSchool.Domain.Entities.Grade;
using LuminiSchool.Domain.Model.Grade.DTOs;
using LuminiSchool.Infrastructure.Repositories.Contract;


namespace LuminiSchool.Business.Services.Implementation
{
    public class GradeService : IGradeService
    {
        private readonly IGradeRepository _r; private readonly IMapper _m;
        public GradeService(IGradeRepository r, IMapper m) { _r = r; _m = m; }
        public async Task<IEnumerable<GradeDto>> GetAllAsync() => _m.Map<IEnumerable<GradeDto>>(await _r.GetAllAsync());
        public async Task<GradeDto> GetByIdAsync(Guid id) => _m.Map<GradeDto>(await _r.GetByIdAsync(id) ?? throw new NotFoundException($"Grado {id} no encontrado."));
        public async Task<GradeDto> CreateAsync(CreateGradeDto dto) { var e = _m.Map<GradeEntity>(dto); e.Id = Guid.NewGuid(); return _m.Map<GradeDto>(await _r.AddAsync(e)); }
        public async Task<GradeDto> UpdateAsync(Guid id, UpdateGradeDto dto) { var e = await _r.GetByIdAsync(id) ?? throw new NotFoundException($"Grado {id} no encontrado."); _m.Map(dto, e); await _r.UpdateAsync(e); return _m.Map<GradeDto>(e); }
        public async Task DeleteAsync(Guid id) { if (!await _r.ExistsAsync(id)) throw new NotFoundException($"Grado {id} no encontrado."); await _r.DeleteAsync(id); }
        public async Task AssignStudentAsync(Guid gid, Guid sid) => await _r.AssignStudentAsync(gid, sid);
        public async Task RemoveStudentAsync(Guid gid, Guid sid) => await _r.RemoveStudentAsync(gid, sid);
        public async Task AssignSubjectAsync(Guid gid, Guid sid) => await _r.AssignSubjectAsync(gid, sid);
        public async Task AssignTeacherAsync(Guid gid, Guid tid) => await _r.AssignTeacherAsync(gid, tid);
    }
}
