using LuminiSchool.Domain.Entities.Attendance;
using LuminiSchool.Infrastructure.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Infrastructure.Repositories.Implementation
{
    public class AttendanceRepository : GenericRepository<AttendanceEntity>, IAttendanceRepository
    {
        public AttendanceRepository(ApplicationDbContext ctx) : base(ctx) { }
        public async Task<IEnumerable<AttendanceEntity>> GetByStudentAsync(Guid sid) => await _db.Where(a => a.StudentId == sid).Include(a => a.Subject).OrderByDescending(a => a.Date).ToListAsync();
        public async Task<IEnumerable<AttendanceEntity>> GetByDateAndGradeAsync(DateTime date, Guid gid) => await _db.Where(a => a.Date.Date == date.Date && a.GradeId == gid).Include(a => a.Student).ToListAsync();
        public async Task<IEnumerable<AttendanceEntity>> GetByStudentAndPeriodAsync(Guid sid, DateTime from, DateTime to) => await _db.Where(a => a.StudentId == sid && a.Date >= from && a.Date <= to).ToListAsync();
    }
}
