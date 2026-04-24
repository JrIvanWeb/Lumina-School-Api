using LuminiSchool.Domain.Model.Subject.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Business.Services.Contract
{
    public interface ISubjectService
    {
        Task<IEnumerable<SubjectDto>> GetAllAsync();
        Task<SubjectDto> GetByIdAsync(Guid id);
        Task<SubjectDto> CreateAsync(CreateSubjectDto dto);
        Task<SubjectDto> UpdateAsync(Guid id, UpdateSubjectDto dto);
        Task DeleteAsync(Guid id);
    }
}
