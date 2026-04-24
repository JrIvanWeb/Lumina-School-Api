using AutoMapper;
using LuminiSchool.Business.Exceptions;
using LuminiSchool.Business.Services.Contract;
using LuminiSchool.Domain.Entities.Observer;
using LuminiSchool.Domain.Model.Observer.DTOs;
using LuminiSchool.Infrastructure.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Business.Services.Implementation
{
    public class ObserverService : IObserverService
    {
        private readonly IObserverRepository _r; private readonly IMapper _m;
        public ObserverService(IObserverRepository r, IMapper m) { _r = r; _m = m; }
        public async Task<IEnumerable<ObserverDto>> GetByStudentAsync(Guid sid) => _m.Map<IEnumerable<ObserverDto>>(await _r.GetByStudentAsync(sid));
        public async Task<ObserverDto> CreateAsync(CreateObserverDto dto) { var e = _m.Map<ObserverEntity>(dto); e.Id = Guid.NewGuid(); e.Date = DateTime.UtcNow; return _m.Map<ObserverDto>(await _r.AddAsync(e)); }
        public async Task DeleteAsync(Guid id) { if (!await _r.ExistsAsync(id)) throw new NotFoundException($"Observación {id} no encontrada."); await _r.DeleteAsync(id); }
    }
}
