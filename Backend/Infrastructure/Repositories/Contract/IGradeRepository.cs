using LuminiSchool.Domain.Entities.Grade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Infrastructure.Repositories.Contract
{
    public interface IGradeRepository : IGenericRepository<GradeEntity>
    {
        Task<IEnumerable<GradeEntity>> GetWithStudentsAsync();
        Task AssignStudentAsync(Guid gradeId, Guid studentId);
        Task RemoveStudentAsync(Guid gradeId, Guid studentId);
        Task AssignSubjectAsync(Guid gradeId, Guid subjectId);
        Task AssignTeacherAsync(Guid gradeId, Guid teacherId);
    }
}
