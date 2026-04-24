using LuminiSchool.Domain.Entities.Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Infrastructure.Repositories.Contract
{
    public interface IScheduleRepository : IGenericRepository<ScheduleEntity>
    {
        Task<IEnumerable<ScheduleEntity>> GetByGradeAsync(Guid gradeId);
        Task<IEnumerable<ScheduleEntity>> GetByTeacherAsync(Guid teacherId);
        Task<bool> HasConflictAsync(Guid gradeId, DayOfWeek day, TimeSpan start, TimeSpan end, Guid? excludeId = null);
    }
}
