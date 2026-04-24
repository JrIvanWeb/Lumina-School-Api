using AutoMapper;
using LuminiSchool.Business.Exceptions;
using LuminiSchool.Business.Services.Contract;
using LuminiSchool.Domain.Entities.Subject;
using LuminiSchool.Domain.Model.Subject.DTOs;
using LuminiSchool.Infrastructure.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Business.Services.Implementation
{
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _r; private readonly IMapper _m;
        public SubjectService(ISubjectRepository r, IMapper m) { _r = r; _m = m; }
        public async Task<IEnumerable<SubjectDto>> GetAllAsync() => _m.Map<IEnumerable<SubjectDto>>(await _r.GetAllAsync());
        public async Task<SubjectDto> GetByIdAsync(Guid id) => _m.Map<SubjectDto>(await _r.GetByIdAsync(id) ?? throw new NotFoundException($"Asignatura {id} no encontrada."));
        public async Task<SubjectDto> CreateAsync(CreateSubjectDto dto) { var e = _m.Map<SubjectEntity>(dto); e.Id = Guid.NewGuid(); return _m.Map<SubjectDto>(await _r.AddAsync(e)); }
        public async Task<SubjectDto> UpdateAsync(Guid id, UpdateSubjectDto dto) { var e = await _r.GetByIdAsync(id) ?? throw new NotFoundException($"Asignatura {id} no encontrada."); _m.Map(dto, e); await _r.UpdateAsync(e); return _m.Map<SubjectDto>(e); }
        public async Task DeleteAsync(Guid id) { if (!await _r.ExistsAsync(id)) throw new NotFoundException($"Asignatura {id} no encontrada."); await _r.DeleteAsync(id); }
    }
}
