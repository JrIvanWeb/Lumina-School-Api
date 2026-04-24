using LuminiSchool.Domain.Entities.Teacher;
using LuminiSchool.Infrastructure.Repositories.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Infrastructure.Repositories.Implementation
{
    public class TeacherRepository : GenericRepository<TeacherEntity>, ITeacherRepository
    {
        public TeacherRepository(ApplicationDbContext ctx) : base(ctx) { }
        public async Task<TeacherEntity?> GetByDocumentAsync(string doc) => await _db.FirstOrDefaultAsync(t => t.DocumentNumber == doc);
        public async Task<IEnumerable<TeacherEntity>> GetWithSubjectsAsync() => await _db.Include(t => t.Subjects).ToListAsync();
        public async Task AssignSubjectAsync(Guid tid, Guid sid) { var t = await _db.Include(x => x.Subjects).FirstOrDefaultAsync(x => x.Id == tid); var s = await _ctx.Subjects.FindAsync(sid); if (t != null && s != null) { t.Subjects.Add(s); await _ctx.SaveChangesAsync(); } }
        public async Task RemoveSubjectAsync(Guid tid, Guid sid) { var t = await _db.Include(x => x.Subjects).FirstOrDefaultAsync(x => x.Id == tid); var s = t?.Subjects.FirstOrDefault(x => x.Id == sid); if (t != null && s != null) { t.Subjects.Remove(s); await _ctx.SaveChangesAsync(); } }
    }
}
