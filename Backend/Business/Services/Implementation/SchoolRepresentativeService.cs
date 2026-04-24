using AutoMapper;
using LuminiSchool.Business.Exceptions;
using LuminiSchool.Business.Services.Contract;
using LuminiSchool.Domain.Entities.SchoolRepresentative;
using LuminiSchool.Domain.Model.SchoolRepresentative.DTOs;
using LuminiSchool.Infrastructure.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Business.Services.Implementation
{
    public class SchoolRepresentativeService : ISchoolRepresentativeService
    {
        private readonly ISchoolRepresentativeRepository _r; private readonly IMapper _m;
        public SchoolRepresentativeService(ISchoolRepresentativeRepository r, IMapper m) { _r = r; _m = m; }
        public async Task<IEnumerable<SchoolRepresentativeDto>> GetActiveAsync() => _m.Map<IEnumerable<SchoolRepresentativeDto>>(await _r.GetActiveAsync());
        public async Task<IEnumerable<SchoolRepresentativeDto>> GetByYearAsync(int y) => _m.Map<IEnumerable<SchoolRepresentativeDto>>(await _r.GetByYearAsync(y));
        public async Task<SchoolRepresentativeDto> CreateAsync(CreateSchoolRepresentativeDto dto) { var e = _m.Map<SchoolRepresentativeEntity>(dto); e.Id = Guid.NewGuid(); return _m.Map<SchoolRepresentativeDto>(await _r.AddAsync(e)); }
        public async Task DeleteAsync(Guid id) { if (!await _r.ExistsAsync(id)) throw new NotFoundException($"Representante {id} no encontrado."); await _r.DeleteAsync(id); }
    }
}
