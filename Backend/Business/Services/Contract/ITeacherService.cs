using LuminiSchool.Domain.Model.Teacher.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Business.Services.Contract
{
    public interface ITeacherService
    {
        Task<IEnumerable<TeacherDto>> GetAllAsync();
        Task<TeacherDto> GetByIdAsync(Guid id);
        Task<TeacherDto> CreateAsync(CreateTeacherDto dto);
        Task<TeacherDto> UpdateAsync(Guid id, UpdateTeacherDto dto);
        Task DeleteAsync(Guid id);
        Task AssignSubjectAsync(Guid teacherId, Guid subjectId);
        Task RemoveSubjectAsync(Guid teacherId, Guid subjectId);
    }
}
