using LuminiSchool.Domain.Entities.GradeRecord;
using LuminiSchool.Infrastructure.Repositories.Contract;
using Microsoft.EntityFrameworkCore;


namespace LuminiSchool.Infrastructure.Repositories.Implementation
{
    public class GradeRecordRepository : GenericRepository<GradeRecordEntity>, IGradeRecordRepository
    {
        public GradeRecordRepository(ApplicationDbContext ctx) : base(ctx) { }
        public async Task<IEnumerable<GradeRecordEntity>> GetByStudentAndPeriodAsync(Guid sid, Guid pid) => await _db.Where(g => g.StudentId == sid && g.AcademicPeriodId == pid).Include(g => g.Subject).ToListAsync();
        public async Task<IEnumerable<GradeRecordEntity>> GetBySubjectAndPeriodAsync(Guid sid, Guid pid) => await _db.Where(g => g.SubjectId == sid && g.AcademicPeriodId == pid).Include(g => g.Student).ToListAsync();
        public async Task<decimal?> GetAverageByStudentAndPeriodAsync(Guid sid, Guid pid) { var r = await _db.Where(g => g.StudentId == sid && g.AcademicPeriodId == pid).ToListAsync(); return r.Any() ? r.Average(g => g.Score) : null; }
    }
}
