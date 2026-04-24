using AutoMapper;
using LuminiSchool.Business.Exceptions;
using LuminiSchool.Business.Services.Contract;
using LuminiSchool.Domain.Entities.Schedule;
using LuminiSchool.Domain.Model.Schedule.DTOs;
using LuminiSchool.Infrastructure.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Business.Services.Implementation
{
    public class ScheduleService : IScheduleService
    {
        private readonly IScheduleRepository _r; private readonly IMapper _m;
        public ScheduleService(IScheduleRepository r, IMapper m) { _r = r; _m = m; }
        public async Task<IEnumerable<ScheduleDto>> GetByGradeAsync(Guid gid) => _m.Map<IEnumerable<ScheduleDto>>(await _r.GetByGradeAsync(gid));
        public async Task<IEnumerable<ScheduleDto>> GetByTeacherAsync(Guid tid) => _m.Map<IEnumerable<ScheduleDto>>(await _r.GetByTeacherAsync(tid));
        public async Task<ScheduleDto> CreateAsync(CreateScheduleDto dto) { if (await _r.HasConflictAsync(dto.GradeId, dto.DayOfWeek, dto.StartTime, dto.EndTime)) throw new BusinessException("Conflicto de horario detectado."); var e = _m.Map<ScheduleEntity>(dto); e.Id = Guid.NewGuid(); return _m.Map<ScheduleDto>(await _r.AddAsync(e)); }
        public async Task DeleteAsync(Guid id) { if (!await _r.ExistsAsync(id)) throw new NotFoundException($"Horario {id} no encontrado."); await _r.DeleteAsync(id); }
    }
}
