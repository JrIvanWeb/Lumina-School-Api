using LuminiSchool.Domain.Entities.Guardian;
using LuminiSchool.Infrastructure.Repositories.Contract;
using Microsoft.EntityFrameworkCore;

namespace LuminiSchool.Infrastructure.Repositories.Implementation
{
    public class GuardianRepository : GenericRepository<GuardianEntity>, IGuardianRepository
    {
        public GuardianRepository(ApplicationDbContext ctx) : base(ctx) { }

        public async Task<IEnumerable<GuardianEntity>> GetByStudentAsync(Guid sid) =>
            await _db.Where(g => g.Students.Any(s => s.Id == sid))
                     .Include(g => g.Students)
                     .Include(g => g.Parent)
                     .ToListAsync();

        public async Task<GuardianEntity?> GetByDocumentAsync(string documentNumber) =>
            await _db.Include(g => g.Students)
                     .FirstOrDefaultAsync(g => g.DocumentNumber == documentNumber);

        public async Task AssignStudentAsync(Guid gid, Guid sid)
        {
            var g = await _db.Include(x => x.Students).FirstOrDefaultAsync(x => x.Id == gid);
            var s = await _ctx.Students.FindAsync(sid);
            if (g != null && s != null) { g.Students.Add(s); await _ctx.SaveChangesAsync(); }
        }

        public async Task AddOrUpdateAsync(GuardianEntity guardian)
        {
            var exists = await _db.AnyAsync(g => g.Id == guardian.Id);
            if (exists) _ctx.Entry(guardian).State = EntityState.Modified;
            else        await _db.AddAsync(guardian);
            await _ctx.SaveChangesAsync();
        }
    }
}
