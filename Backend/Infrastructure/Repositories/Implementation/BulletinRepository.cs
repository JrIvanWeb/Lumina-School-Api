using LuminiSchool.Domain.Entities.Bulletin;
using LuminiSchool.Infrastructure.Repositories.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Infrastructure.Repositories.Implementation
{
    public class BulletinRepository : GenericRepository<BulletinEntity>, IBulletinRepository
    {
        public BulletinRepository(ApplicationDbContext ctx) : base(ctx) { }
        public async Task<IEnumerable<BulletinEntity>> GetByStudentAsync(Guid sid) => await _db.Where(b => b.StudentId == sid).ToListAsync();
        public async Task<BulletinEntity?> GetByStudentAndPeriodAsync(Guid sid, Guid pid) => await _db.FirstOrDefaultAsync(b => b.StudentId == sid && b.AcademicPeriodId == pid);
    }
}
