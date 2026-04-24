using LuminiSchool.Domain.Entities.Guardian;
using LuminiSchool.Infrastructure.Repositories.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Infrastructure.Repositories.Implementation
{
    public class GuardianRepository : GenericRepository<GuardianEntity>, IGuardianRepository
    {
        public GuardianRepository(ApplicationDbContext ctx) : base(ctx) { }
        public async Task<IEnumerable<GuardianEntity>> GetByStudentAsync(Guid sid) => await _db.Where(g => g.Students.Any(s => s.Id == sid)).Include(g => g.Students).ToListAsync();
        public async Task AssignStudentAsync(Guid gid, Guid sid) { var g = await _db.Include(x => x.Students).FirstOrDefaultAsync(x => x.Id == gid); var s = await _ctx.Students.FindAsync(sid); if (g != null && s != null) { g.Students.Add(s); await _ctx.SaveChangesAsync(); } }
    }
}
