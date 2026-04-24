using LuminiSchool.Domain.Entities.DiagnosticTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Infrastructure.Repositories.Contract
{
    public interface IDiagnosticTestRepository : IGenericRepository<DiagnosticTestEntity>
    {
        Task<IEnumerable<DiagnosticTestEntity>> GetByTeacherAsync(Guid teacherId);
        Task<IEnumerable<DiagnosticTestEntity>> GetByGradeAsync(Guid gradeId);
        Task<IEnumerable<DiagnosticResultEntity>> GetResultsAsync(Guid testId);
        Task<DiagnosticResultEntity> AddResultAsync(DiagnosticResultEntity result);
    }
}
