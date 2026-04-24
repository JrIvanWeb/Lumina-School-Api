using AutoMapper;
using LuminiSchool.Business.Exceptions;
using LuminiSchool.Business.Services.Contract;
using LuminiSchool.Domain.Entities.Achievement;
using LuminiSchool.Domain.Model.Achievement.DTOs;
using LuminiSchool.Infrastructure.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Business.Services.Implementation
{
    public class AchievementService : IAchievementService
    {
        private readonly IAchievementRepository _r; private readonly IMapper _m;
        public AchievementService(IAchievementRepository r, IMapper m) { _r = r; _m = m; }
        public async Task<IEnumerable<AchievementDto>> GetAllAsync() => _m.Map<IEnumerable<AchievementDto>>(await _r.GetAllAsync());
        public async Task<IEnumerable<AchievementDto>> GetBySubjectAsync(Guid sid) => _m.Map<IEnumerable<AchievementDto>>(await _r.GetBySubjectAsync(sid));
        public async Task<AchievementDto> CreateAsync(CreateAchievementDto dto) { var e = _m.Map<AchievementEntity>(dto); e.Id = Guid.NewGuid(); return _m.Map<AchievementDto>(await _r.AddAsync(e)); }
        public async Task DeleteAsync(Guid id) { if (!await _r.ExistsAsync(id)) throw new NotFoundException($"Logro {id} no encontrado."); await _r.DeleteAsync(id); }
    }
}
