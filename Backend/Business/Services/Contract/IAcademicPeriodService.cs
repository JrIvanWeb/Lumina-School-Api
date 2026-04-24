using LuminiSchool.Domain.Model.AcademicPeriod.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Business.Services.Contract
{
    public interface IAcademicPeriodService
    {
        Task<IEnumerable<AcademicPeriodDto>> GetAllAsync();
        Task<AcademicPeriodDto?> GetActiveAsync();
        Task<IEnumerable<AcademicPeriodDto>> GetByYearAsync(int year);
        Task<AcademicPeriodDto> CreateAsync(CreateAcademicPeriodDto dto);
        Task DeleteAsync(Guid id);
    }
}
