using LuminiSchool.Domain.Model.Grade.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Business.Services.Contract
{
    public interface IGradeService
    {
        Task<IEnumerable<GradeDto>> GetAllAsync();
        Task<GradeDto> GetByIdAsync(Guid id);
        Task<GradeDto> CreateAsync(CreateGradeDto dto);
        Task<GradeDto> UpdateAsync(Guid id, UpdateGradeDto dto);
        Task DeleteAsync(Guid id);
        Task AssignStudentAsync(Guid gradeId, Guid studentId);
        Task RemoveStudentAsync(Guid gradeId, Guid studentId);
        Task AssignSubjectAsync(Guid gradeId, Guid subjectId);
        Task AssignTeacherAsync(Guid gradeId, Guid teacherId);
    }
}
