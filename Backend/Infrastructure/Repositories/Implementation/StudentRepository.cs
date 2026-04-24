using LuminiSchool.Domain.Entities.Student;
using LuminiSchool.Infrastructure.Repositories.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Infrastructure.Repositories.Implementation
{
    public class StudentRepository : GenericRepository<StudentEntity>, IStudentRepository
    {
        public StudentRepository(ApplicationDbContext ctx) : base(ctx) { }
        public async Task<StudentEntity?> GetByDocumentAsync(string doc) => await _db.FirstOrDefaultAsync(s => s.DocumentNumber == doc);
        public async Task<IEnumerable<StudentEntity>> GetByGradeAsync(Guid gradeId) => await _db.Where(s => s.Grades.Any(g => g.Id == gradeId)).ToListAsync();
        public async Task<IEnumerable<StudentEntity>> GetByStatusAsync(StudentStatus status) => await _db.Where(s => s.Status == status).ToListAsync();
        public async Task<IEnumerable<StudentEntity>> GetBirthdaysTodayAsync() { var t = DateTime.Today; return await _db.Where(s => s.BirthDate.Month == t.Month && s.BirthDate.Day == t.Day).ToListAsync(); }
    }
}
