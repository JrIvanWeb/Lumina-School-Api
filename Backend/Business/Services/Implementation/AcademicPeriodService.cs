using AutoMapper;
using LuminiSchool.Business.Exceptions;
using LuminiSchool.Business.Services.Contract;
using LuminiSchool.Domain.Entities.AcademicPeriod;
using LuminiSchool.Domain.Model.AcademicPeriod.DTOs;
using LuminiSchool.Infrastructure.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Business.Services.Implementation
{
    public class AcademicPeriodService : IAcademicPeriodService
    {
        private readonly IAcademicPeriodRepository _r; private readonly IMapper _m;
        public AcademicPeriodService(IAcademicPeriodRepository r, IMapper m) { _r = r; _m = m; }
        public async Task<IEnumerable<AcademicPeriodDto>> GetAllAsync() => _m.Map<IEnumerable<AcademicPeriodDto>>(await _r.GetAllAsync());
        public async Task<AcademicPeriodDto?> GetActiveAsync() => _m.Map<AcademicPeriodDto?>(await _r.GetActiveAsync());
        public async Task<IEnumerable<AcademicPeriodDto>> GetByYearAsync(int y) => _m.Map<IEnumerable<AcademicPeriodDto>>(await _r.GetByYearAsync(y));
        public async Task<AcademicPeriodDto> CreateAsync(CreateAcademicPeriodDto dto) { var e = _m.Map<AcademicPeriodEntity>(dto); e.Id = Guid.NewGuid(); return _m.Map<AcademicPeriodDto>(await _r.AddAsync(e)); }
        public async Task DeleteAsync(Guid id) { if (!await _r.ExistsAsync(id)) throw new NotFoundException($"Periodo {id} no encontrado."); await _r.DeleteAsync(id); }
    }
}
