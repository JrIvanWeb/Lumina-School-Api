using LuminiSchool.Domain.Entities.GradeRecord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Infrastructure.Repositories.Contract
{
    public interface IGradeRecordRepository : IGenericRepository<GradeRecordEntity>
    {
        Task<IEnumerable<GradeRecordEntity>> GetByStudentAndPeriodAsync(Guid studentId, Guid periodId);
        Task<IEnumerable<GradeRecordEntity>> GetBySubjectAndPeriodAsync(Guid subjectId, Guid periodId);
        Task<decimal?> GetAverageByStudentAndPeriodAsync(Guid studentId, Guid periodId);
    }
}
