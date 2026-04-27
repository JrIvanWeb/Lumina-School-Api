using LuminiSchool.Domain.Entities.Student;
using LuminiSchool.Infrastructure.Repositories.Contract;
using Microsoft.EntityFrameworkCore;

namespace LuminiSchool.Infrastructure.Repositories.Implementation
{
    public class StudentRepository : GenericRepository<StudentEntity>, IStudentRepository
    {
        public StudentRepository(ApplicationDbContext ctx) : base(ctx) { }

        public async Task<StudentEntity?> GetByDocumentAsync(string documentNumber) =>
            await _db.Include(s => s.Guardians)
                     .Include(s => s.Parents)
                     .FirstOrDefaultAsync(s => s.DocumentNumber == documentNumber);

        public async Task<IEnumerable<StudentEntity>> GetByGradeAsync(Guid gradeId) =>
            await _db.Where(s => s.Grades.Any(g => g.Id == gradeId)).ToListAsync();

        public async Task<IEnumerable<StudentEntity>> GetBirthdaysTodayAsync()
        {
            var today = DateTime.UtcNow;
            return await _db
                .Where(s => s.BirthDate.Month == today.Month && s.BirthDate.Day == today.Day)
                .ToListAsync();
        }
    }
}
