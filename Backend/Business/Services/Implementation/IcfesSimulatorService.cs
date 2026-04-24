using AutoMapper;
using LuminiSchool.Business.Exceptions;
using LuminiSchool.Business.Services.Contract;
using LuminiSchool.Domain.Entities.IcfesSimulator;
using LuminiSchool.Domain.Model.IcfesSimulator.DTOs;
using LuminiSchool.Infrastructure.Repositories.Contract;


namespace LuminiSchool.Business.Services.Implementation
{
    public class IcfesSimulatorService : IIcfesSimulatorService
    {
        private readonly IIcfesSimulatorRepository _r; private readonly IMapper _m;
        public IcfesSimulatorService(IIcfesSimulatorRepository r, IMapper m) { _r = r; _m = m; }
        public async Task<IEnumerable<IcfesSimulatorDto>> GetAllAsync() => _m.Map<IEnumerable<IcfesSimulatorDto>>(await _r.GetAllAsync());
        public async Task<IcfesSimulatorDto> GetByIdAsync(Guid id) => _m.Map<IcfesSimulatorDto>(await _r.GetByIdAsync(id) ?? throw new NotFoundException($"Simulacro {id} no encontrado."));
        public async Task<IcfesSimulatorDto> CreateAsync(CreateIcfesSimulatorDto dto, Guid cid) { var e = _m.Map<IcfesSimulatorEntity>(dto); e.Id = Guid.NewGuid(); e.CoordinatorId = cid; return _m.Map<IcfesSimulatorDto>(await _r.AddAsync(e)); }
        public async Task ActivateAsync(Guid id) { var e = await _r.GetByIdAsync(id) ?? throw new NotFoundException($"Simulacro {id} no encontrado."); e.Status = IcfesSimulatorStatus.Active; await _r.UpdateAsync(e); }
        public async Task CompleteAsync(Guid id) { var e = await _r.GetByIdAsync(id) ?? throw new NotFoundException($"Simulacro {id} no encontrado."); e.Status = IcfesSimulatorStatus.Completed; await _r.UpdateAsync(e); }
        public async Task<IcfesResultDto> RegisterResultAsync(CreateIcfesResultDto dto) { var e = _m.Map<IcfesResultEntity>(dto); e.Id = Guid.NewGuid(); return _m.Map<IcfesResultDto>(await _r.AddResultAsync(e)); }
        public async Task<IEnumerable<IcfesResultDto>> GetResultsBySimulatorAsync(Guid sid) => _m.Map<IEnumerable<IcfesResultDto>>(await _r.GetResultsBySimulatorAsync(sid));
        public async Task<IEnumerable<IcfesResultDto>> GetResultsByStudentAsync(Guid sid) => _m.Map<IEnumerable<IcfesResultDto>>(await _r.GetResultsByStudentAsync(sid));
    }
}
