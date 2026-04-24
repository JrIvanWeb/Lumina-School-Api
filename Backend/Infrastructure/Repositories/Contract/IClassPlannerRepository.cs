using LuminiSchool.Domain.Entities.ClassPlanner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Infrastructure.Repositories.Contract
{
    public interface IClassPlannerRepository : IGenericRepository<ClassPlannerEntity>
    {
        Task<IEnumerable<ClassPlannerEntity>> GetByTeacherAsync(Guid teacherId);
        Task<IEnumerable<ClassPlannerEntity>> GetByGradeAndPeriodAsync(Guid gradeId, Guid periodId);
    }
}
