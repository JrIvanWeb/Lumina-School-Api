using LuminiSchool.Domain.Entities.GradeSubjectTeacher;
using LuminiSchool.Infrastructure.Repositories.Contract;
using Microsoft.EntityFrameworkCore;

namespace LuminiSchool.Infrastructure.Repositories.Implementation
{
    public class GradeSubjectTeacherRepository : IGradeSubjectTeacherRepository
    {
        private readonly ApplicationDbContext _ctx;

        public GradeSubjectTeacherRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<IEnumerable<GradeSubjectTeacherEntity>> GetAllAsync() =>
            await _ctx.GradeSubjectTeachers
                .Include(x => x.Grade)
                .Include(x => x.Subject)
                .Include(x => x.Teacher)
                .ToListAsync();

        public async Task<IEnumerable<GradeSubjectTeacherEntity>> GetByGradeAsync(Guid gradeId) =>
            await _ctx.GradeSubjectTeachers
                .Include(x => x.Grade)
                .Include(x => x.Subject)
                .Include(x => x.Teacher)
                .Where(x => x.GradeId == gradeId)
                .ToListAsync();

        public async Task<GradeSubjectTeacherEntity?> GetByIdAsync(Guid id) =>
            await _ctx.GradeSubjectTeachers
                .Include(x => x.Grade)
                .Include(x => x.Subject)
                .Include(x => x.Teacher)
                .FirstOrDefaultAsync(x => x.Id == id);

        public async Task<bool> ExistsAsync(Guid gradeId, Guid subjectId, Guid teacherId) =>
            await _ctx.GradeSubjectTeachers
                .AnyAsync(x => x.GradeId == gradeId && x.SubjectId == subjectId && x.TeacherId == teacherId);

        public async Task<GradeSubjectTeacherEntity> AddAsync(GradeSubjectTeacherEntity entity)
        {
            _ctx.GradeSubjectTeachers.Add(entity);
            await _ctx.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(GradeSubjectTeacherEntity entity)
        {
            _ctx.GradeSubjectTeachers.Update(entity);
            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _ctx.GradeSubjectTeachers.FindAsync(id);
            if (entity != null)
            {
                _ctx.GradeSubjectTeachers.Remove(entity);
                await _ctx.SaveChangesAsync();
            }
        }
    }
}
