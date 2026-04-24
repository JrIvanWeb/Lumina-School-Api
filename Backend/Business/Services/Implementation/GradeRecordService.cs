using AutoMapper;
using LuminiSchool.Business.Exceptions;
using LuminiSchool.Business.Services.Contract;
using LuminiSchool.Domain.Entities.GradeRecord;
using LuminiSchool.Domain.Model.GradeRecord.DTOs;
using LuminiSchool.Infrastructure.Repositories.Contract;


namespace LuminiSchool.Business.Services.Implementation
{
    public class GradeRecordService : IGradeRecordService
    {
        private readonly IGradeRecordRepository _r; private readonly IMapper _m;
        public GradeRecordService(IGradeRecordRepository r, IMapper m) { _r = r; _m = m; }
        public async Task<IEnumerable<GradeRecordDto>> GetByStudentAndPeriodAsync(Guid sid, Guid pid) => _m.Map<IEnumerable<GradeRecordDto>>(await _r.GetByStudentAndPeriodAsync(sid, pid));
        public async Task<IEnumerable<GradeRecordDto>> GetBySubjectAndPeriodAsync(Guid sid, Guid pid) => _m.Map<IEnumerable<GradeRecordDto>>(await _r.GetBySubjectAndPeriodAsync(sid, pid));
        public async Task<GradeRecordDto> RegisterAsync(CreateGradeRecordDto dto) { var e = _m.Map<GradeRecordEntity>(dto); e.Id = Guid.NewGuid(); return _m.Map<GradeRecordDto>(await _r.AddAsync(e)); }
        public async Task<GradeRecordDto> UpdateAsync(Guid id, UpdateGradeRecordDto dto) { var e = await _r.GetByIdAsync(id) ?? throw new NotFoundException($"Calificación {id} no encontrada."); _m.Map(dto, e); e.UpdatedAt = DateTime.UtcNow; await _r.UpdateAsync(e); return _m.Map<GradeRecordDto>(e); }
        public async Task<decimal?> GetAverageAsync(Guid sid, Guid pid) => await _r.GetAverageByStudentAndPeriodAsync(sid, pid);
    }
}
