using LuminiSchool.Domain.Entities.Achievement;
using LuminiSchool.Infrastructure.Repositories.Contract;
using Microsoft.EntityFrameworkCore;

namespace LuminiSchool.Infrastructure.Repositories.Implementation
{
    public class AchievementRepository
        : GenericRepository<AchievementEntity>, IAchievementRepository
    {
        public AchievementRepository(ApplicationDbContext ctx) : base(ctx) { }

        // ── Base query con relaciones incluidas ──────────────────────────────
        private IQueryable<AchievementEntity> WithDetails()
            => _db
                .Include(a => a.Period)
                .Include(a => a.Grade)
                .Include(a => a.Subject);

        // ── GetAllWithDetailsAsync ───────────────────────────────────────────
        public async Task<IEnumerable<AchievementEntity>> GetAllWithDetailsAsync()
            => await WithDetails()
                .Where(a => a.IsActive)
                .OrderBy(a => a.GradeId)
                .ThenBy(a => a.SubjectId)
                .ThenBy(a => a.Performance)
                .ToListAsync();

        // ── GetFilteredAsync ─────────────────────────────────────────────────
        public async Task<IEnumerable<AchievementEntity>> GetFilteredAsync(
            Guid?             periodId,
            Guid?             gradeId,
            Guid?             subjectId,
            PerformanceLevel? performance)
        {
            var q = WithDetails().Where(a => a.IsActive);

            if (periodId.HasValue)    q = q.Where(a => a.PeriodId    == periodId.Value);
            if (gradeId.HasValue)     q = q.Where(a => a.GradeId     == gradeId.Value);
            if (subjectId.HasValue)   q = q.Where(a => a.SubjectId   == subjectId.Value);
            if (performance.HasValue) q = q.Where(a => a.Performance == performance.Value);

            return await q
                .OrderBy(a => a.Performance)
                .ThenBy(a => a.CreatedAt)
                .ToListAsync();
        }

        // ── GetByIdWithDetailsAsync ──────────────────────────────────────────
        public async Task<AchievementEntity?> GetByIdWithDetailsAsync(Guid id)
            => await WithDetails().FirstOrDefaultAsync(a => a.Id == id);
    }
}
