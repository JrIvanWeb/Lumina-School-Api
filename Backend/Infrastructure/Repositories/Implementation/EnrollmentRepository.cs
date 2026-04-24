using LuminiSchool.Domain.Entities.Enrollment;
using LuminiSchool.Infrastructure.Repositories.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Infrastructure.Repositories.Implementation
{
    public class EnrollmentRepository : GenericRepository<EnrollmentEntity>, IEnrollmentRepository
    {
        public EnrollmentRepository(ApplicationDbContext ctx) : base(ctx) { }
        public async Task<IEnumerable<EnrollmentEntity>> GetByStudentAsync(Guid sid) => await _db.Where(e => e.StudentId == sid).Include(e => e.Grade).ToListAsync();
        public async Task<IEnumerable<EnrollmentEntity>> GetByGradeAndYearAsync(Guid gid, int year) => await _db.Where(e => e.GradeId == gid && e.AcademicYear == year).Include(e => e.Student).ToListAsync();
        public async Task<EnrollmentEntity?> GetActiveByStudentAsync(Guid sid) => await _db.FirstOrDefaultAsync(e => e.StudentId == sid && e.Status == EnrollmentStatus.Active);
    }
}
