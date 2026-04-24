using LuminiSchool.Domain.Model.Student.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Business.Services.Contract
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentDto>> GetAllAsync();
        Task<StudentDto> GetByIdAsync(Guid id);
        Task<StudentDto> CreateAsync(CreateStudentDto dto);
        Task<StudentDto> UpdateAsync(Guid id, UpdateStudentDto dto);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<StudentDto>> GetBirthdaysTodayAsync();
        Task<IEnumerable<StudentDto>> GetByGradeAsync(Guid gradeId);
    }
}
