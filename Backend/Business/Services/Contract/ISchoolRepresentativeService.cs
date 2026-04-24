using LuminiSchool.Domain.Model.SchoolRepresentative.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Business.Services.Contract
{
    public interface ISchoolRepresentativeService
    {
        Task<IEnumerable<SchoolRepresentativeDto>> GetActiveAsync();
        Task<IEnumerable<SchoolRepresentativeDto>> GetByYearAsync(int year);
        Task<SchoolRepresentativeDto> CreateAsync(CreateSchoolRepresentativeDto dto);
        Task DeleteAsync(Guid id);
    }
}
