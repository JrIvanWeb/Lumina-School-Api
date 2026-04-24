using LuminiSchool.Domain.Entities.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Infrastructure.Repositories.Contract
{
    public interface IStudentRepository : IGenericRepository<StudentEntity>
    {
        Task<StudentEntity?> GetByDocumentAsync(string doc);
        Task<IEnumerable<StudentEntity>> GetByGradeAsync(Guid gradeId);
        Task<IEnumerable<StudentEntity>> GetByStatusAsync(StudentStatus status);
        Task<IEnumerable<StudentEntity>> GetBirthdaysTodayAsync();
    }
}
