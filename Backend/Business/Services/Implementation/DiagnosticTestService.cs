using AutoMapper;
using LuminiSchool.Business.Exceptions;
using LuminiSchool.Business.Services.Contract;
using LuminiSchool.Domain.Entities.DiagnosticTest;
using LuminiSchool.Domain.Model.DiagnosticTest.DTOs;
using LuminiSchool.Infrastructure.Repositories.Contract;


namespace LuminiSchool.Business.Services.Implementation
{
    public class DiagnosticTestService : IDiagnosticTestService
    {
        private readonly IDiagnosticTestRepository _r; private readonly IMapper _m;
        public DiagnosticTestService(IDiagnosticTestRepository r, IMapper m) { _r = r; _m = m; }
        public async Task<IEnumerable<DiagnosticTestDto>> GetAllAsync() => _m.Map<IEnumerable<DiagnosticTestDto>>(await _r.GetAllAsync());
        public async Task<DiagnosticTestDto> GetByIdAsync(Guid id) => _m.Map<DiagnosticTestDto>(await _r.GetByIdAsync(id) ?? throw new NotFoundException($"Prueba {id} no encontrada."));
        public async Task<IEnumerable<DiagnosticTestDto>> GetByTeacherAsync(Guid tid) => _m.Map<IEnumerable<DiagnosticTestDto>>(await _r.GetByTeacherAsync(tid));
        public async Task<DiagnosticTestDto> CreateAsync(CreateDiagnosticTestDto dto) { var e = _m.Map<DiagnosticTestEntity>(dto); e.Id = Guid.NewGuid(); return _m.Map<DiagnosticTestDto>(await _r.AddAsync(e)); }
        public async Task DeleteAsync(Guid id) { if (!await _r.ExistsAsync(id)) throw new NotFoundException($"Prueba {id} no encontrada."); await _r.DeleteAsync(id); }
        public async Task<DiagnosticResultDto> RegisterResultAsync(CreateDiagnosticResultDto dto) { var e = _m.Map<DiagnosticResultEntity>(dto); e.Id = Guid.NewGuid(); return _m.Map<DiagnosticResultDto>(await _r.AddResultAsync(e)); }
        public async Task<IEnumerable<DiagnosticResultDto>> GetResultsAsync(Guid tid) => _m.Map<IEnumerable<DiagnosticResultDto>>(await _r.GetResultsAsync(tid));
    }
}
