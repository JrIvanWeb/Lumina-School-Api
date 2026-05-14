using LuminiSchool.Domain.Entities.Enrollment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Infrastructure.Repositories.Contract
{
    public interface IEnrollmentRepository : IGenericRepository<EnrollmentEntity>
    {
        Task<IEnumerable<EnrollmentEntity>> GetByStudentAsync(Guid studentId);
        Task<IEnumerable<EnrollmentEntity>> GetByGradeAndYearAsync(Guid gradeId, int year);
        Task<EnrollmentEntity?> GetActiveByStudentAsync(Guid studentId);
        Task<IEnumerable<EnrollmentEntity>> GetAllWithDetailsAsync();
        Task<EnrollmentEntity?> GetWithDetailsAsync(Guid enrollmentId);
    }
}
