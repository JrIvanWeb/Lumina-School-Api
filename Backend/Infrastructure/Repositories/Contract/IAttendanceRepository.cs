using LuminiSchool.Domain.Entities.Attendance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Infrastructure.Repositories.Contract
{
    public interface IAttendanceRepository : IGenericRepository<AttendanceEntity>
    {
        Task<IEnumerable<AttendanceEntity>> GetByStudentAsync(Guid studentId);
        Task<IEnumerable<AttendanceEntity>> GetByDateAndGradeAsync(DateTime date, Guid gradeId);
        Task<IEnumerable<AttendanceEntity>> GetByStudentAndPeriodAsync(Guid studentId, DateTime from, DateTime to);
    }
}
