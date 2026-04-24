using LuminiSchool.Domain.Entities.Activity;
using LuminiSchool.Infrastructure.Repositories.Contract;
using Microsoft.EntityFrameworkCore;

namespace LuminiSchool.Infrastructure.Repositories.Implementation
{
    public class ActivityRepository : GenericRepository<ActivityEntity>, IActivityRepository
    {
        public ActivityRepository(ApplicationDbContext ctx) : base(ctx) { }
        public async Task<IEnumerable<ActivityEntity>> GetByTeacherAsync(Guid tid) => await _db.Where(a => a.TeacherId == tid).Include(a => a.Subject).ToListAsync();
        public async Task<IEnumerable<ActivityEntity>> GetByGradeAndSubjectAsync(Guid gid, Guid sid) => await _db.Where(a => a.GradeId == gid && a.SubjectId == sid).ToListAsync();
        public async Task<IEnumerable<ActivitySubmissionEntity>> GetSubmissionsAsync(Guid aid) => await _ctx.ActivitySubmissions.Where(s => s.ActivityId == aid).Include(s => s.Student).ToListAsync();
        public async Task<ActivitySubmissionEntity?> GetSubmissionByStudentAsync(Guid aid, Guid sid) => await _ctx.ActivitySubmissions.FirstOrDefaultAsync(s => s.ActivityId == aid && s.StudentId == sid);
        public async Task<ActivitySubmissionEntity> AddSubmissionAsync(ActivitySubmissionEntity s) { await _ctx.ActivitySubmissions.AddAsync(s); await _ctx.SaveChangesAsync(); return s; }
        public async Task UpdateSubmissionAsync(ActivitySubmissionEntity s) { _ctx.ActivitySubmissions.Update(s); await _ctx.SaveChangesAsync(); }
    }
}
