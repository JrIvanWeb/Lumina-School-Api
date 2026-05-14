using LuminiSchool.Domain.Model.AcademicPeriod.DTOs;

namespace LuminiSchool.Business.Services.Contract
{
    public interface IAcademicPeriodService
    {
        Task<IEnumerable<AcademicPeriodDto>> GetAllAsync();
        Task<AcademicPeriodDto?> GetActiveAsync();
        Task<IEnumerable<AcademicPeriodDto>> GetByYearAsync(int year);
        Task<AcademicPeriodDto> CreateAsync(CreateAcademicPeriodDto dto);
        Task<AcademicPeriodDto> UpdateAsync(Guid id, UpdateAcademicPeriodDto dto);
        Task ToggleActiveAsync(Guid id);
        Task DeleteAsync(Guid id);
    }
}
