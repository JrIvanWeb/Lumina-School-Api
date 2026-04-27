using LuminiSchool.Domain.Entities.Schedule;
using LuminiSchool.Infrastructure.Repositories.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Infrastructure.Repositories.Implementation
{
    public class ScheduleRepository : GenericRepository<ScheduleEntity>, IScheduleRepository
    {
        public ScheduleRepository(ApplicationDbContext ctx) : base(ctx) { }
        public async Task<IEnumerable<ScheduleEntity>> GetByGradeAsync(Guid gid) => await _db.Where(s => s.GradeId == gid).Include(s => s.Subject).Include(s => s.Teacher).ToListAsync();
        public async Task<IEnumerable<ScheduleEntity>> GetByTeacherAsync(Guid tid) => await _db.Where(s => s.TeacherId == tid).Include(s => s.Grade).Include(s => s.Subject).ToListAsync();
        public async Task<bool> HasConflictAsync(Guid gid, DayOfWeek day, TimeSpan start, TimeSpan end, Guid? excludeId = null) => await _db.AnyAsync(s => s.GradeId == gid && s.DayOfWeek == day && s.Id != excludeId && s.StartTime < end && s.EndTime > start);
    }
}
