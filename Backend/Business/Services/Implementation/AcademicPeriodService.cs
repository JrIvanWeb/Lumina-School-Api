using AutoMapper;
using LuminiSchool.Business.Exceptions;
using LuminiSchool.Business.Services.Contract;
using LuminiSchool.Domain.Entities.AcademicPeriod;
using LuminiSchool.Domain.Model.AcademicPeriod.DTOs;
using LuminiSchool.Infrastructure.Repositories.Contract;

namespace LuminiSchool.Business.Services.Implementation
{
    public class AcademicPeriodService : IAcademicPeriodService
    {
        private readonly IAcademicPeriodRepository _r;
        private readonly IMapper _m;

        public AcademicPeriodService(IAcademicPeriodRepository r, IMapper m)
        {
            _r = r;
            _m = m;
        }

        public async Task<IEnumerable<AcademicPeriodDto>> GetAllAsync()
            => _m.Map<IEnumerable<AcademicPeriodDto>>(await _r.GetAllAsync());

        public async Task<AcademicPeriodDto?> GetActiveAsync()
            => _m.Map<AcademicPeriodDto?>(await _r.GetActiveAsync());

        public async Task<IEnumerable<AcademicPeriodDto>> GetByYearAsync(int year)
            => _m.Map<IEnumerable<AcademicPeriodDto>>(await _r.GetByYearAsync(year));

        public async Task<AcademicPeriodDto> CreateAsync(CreateAcademicPeriodDto dto)
        {
            var entity = _m.Map<AcademicPeriodEntity>(dto);
            entity.Id = Guid.NewGuid();
            return _m.Map<AcademicPeriodDto>(await _r.AddAsync(entity));
        }

        public async Task<AcademicPeriodDto> UpdateAsync(Guid id, UpdateAcademicPeriodDto dto)
        {
            var entity = await _r.GetByIdAsync(id)
                ?? throw new NotFoundException($"Período {id} no encontrado.");

            entity.AcademicYear  = dto.AcademicYear;
            entity.PeriodNumber  = dto.PeriodNumber;
            entity.StartDate     = dto.StartDate;
            entity.EndDate       = dto.EndDate;
            entity.IsActive      = dto.IsActive;

            await _r.UpdateAsync(entity);
            return _m.Map<AcademicPeriodDto>(entity);
        }

        public async Task ToggleActiveAsync(Guid id)
        {
            var entity = await _r.GetByIdAsync(id)
                ?? throw new NotFoundException($"Período {id} no encontrado.");

            entity.IsActive = !entity.IsActive;
            await _r.UpdateAsync(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            if (!await _r.ExistsAsync(id))
                throw new NotFoundException($"Período {id} no encontrado.");

            await _r.DeleteAsync(id);
        }
    }
}
