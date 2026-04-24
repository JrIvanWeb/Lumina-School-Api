using LuminiSchool.Domain.Entities.ClassPlanner;
using LuminiSchool.Infrastructure.Repositories.Contract;
using Microsoft.EntityFrameworkCore;


namespace LuminiSchool.Infrastructure.Repositories.Implementation
{
    public class ClassPlannerRepository : GenericRepository<ClassPlannerEntity>, IClassPlannerRepository
    {
        public ClassPlannerRepository(ApplicationDbContext ctx) : base(ctx) { }
        public async Task<IEnumerable<ClassPlannerEntity>> GetByTeacherAsync(Guid tid) => await _db.Where(c => c.TeacherId == tid).Include(c => c.Subject).Include(c => c.Grade).ToListAsync();
        public async Task<IEnumerable<ClassPlannerEntity>> GetByGradeAndPeriodAsync(Guid gid, Guid pid) => await _db.Where(c => c.GradeId == gid && c.AcademicPeriodId == pid).ToListAsync();
    }
}
