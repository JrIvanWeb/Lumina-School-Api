using AutoMapper;
using LuminiSchool.Business.Exceptions;
using LuminiSchool.Business.Services.Contract;
using LuminiSchool.Domain.Entities.ClassPlanner;
using LuminiSchool.Domain.Model.ClassPlanner.DTOs;
using LuminiSchool.Infrastructure.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Business.Services.Implementation
{
    public class ClassPlannerService : IClassPlannerService
    {
        private readonly IClassPlannerRepository _r; private readonly IMapper _m;
        public ClassPlannerService(IClassPlannerRepository r, IMapper m) { _r = r; _m = m; }
        public async Task<IEnumerable<ClassPlannerDto>> GetAllAsync() => _m.Map<IEnumerable<ClassPlannerDto>>(await _r.GetAllAsync());
        public async Task<ClassPlannerDto> GetByIdAsync(Guid id) => _m.Map<ClassPlannerDto>(await _r.GetByIdAsync(id) ?? throw new NotFoundException($"Planeador {id} no encontrado."));
        public async Task<IEnumerable<ClassPlannerDto>> GetByTeacherAsync(Guid tid) => _m.Map<IEnumerable<ClassPlannerDto>>(await _r.GetByTeacherAsync(tid));
        public async Task<IEnumerable<ClassPlannerDto>> GetByGradeAndPeriodAsync(Guid gid, Guid pid) => _m.Map<IEnumerable<ClassPlannerDto>>(await _r.GetByGradeAndPeriodAsync(gid, pid));
        public async Task<ClassPlannerDto> CreateAsync(CreateClassPlannerDto dto) { var e = _m.Map<ClassPlannerEntity>(dto); e.Id = Guid.NewGuid(); return _m.Map<ClassPlannerDto>(await _r.AddAsync(e)); }
        public async Task<ClassPlannerDto> UpdateAsync(Guid id, UpdateClassPlannerDto dto) { var e = await _r.GetByIdAsync(id) ?? throw new NotFoundException($"Planeador {id} no encontrado."); _m.Map(dto, e); await _r.UpdateAsync(e); return _m.Map<ClassPlannerDto>(e); }
        public async Task DeleteAsync(Guid id) { if (!await _r.ExistsAsync(id)) throw new NotFoundException($"Planeador {id} no encontrado."); await _r.DeleteAsync(id); }
    }
}
