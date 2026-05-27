using LuminiSchool.Domain.Model.School.DTOs;

namespace LuminiSchool.Infrastructure.Repositories.Contract
{
    public interface ISchoolService
    {
        Task<IEnumerable<SchoolDto>> GetAllAsync();
        Task<SchoolDto> GetByIdAsync(Guid id);
        Task<SchoolDto> CreateAsync(CreateSchoolDto dto);
        Task DeleteAsync(Guid id);
    }
}
