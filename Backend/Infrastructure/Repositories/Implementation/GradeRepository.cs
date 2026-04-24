using LuminiSchool.Domain.Entities.Grade;
using LuminiSchool.Infrastructure.Repositories.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Infrastructure.Repositories.Implementation
{
    public class GradeRepository : GenericRepository<GradeEntity>, IGradeRepository
    {
        public GradeRepository(ApplicationDbContext ctx) : base(ctx) { }
        public async Task<IEnumerable<GradeEntity>> GetWithStudentsAsync() => await _db.Include(g => g.Students).ToListAsync();
        public async Task AssignStudentAsync(Guid gid, Guid sid) { var g = await _db.Include(x => x.Students).FirstOrDefaultAsync(x => x.Id == gid); var s = await _ctx.Students.FindAsync(sid); if (g != null && s != null) { g.Students.Add(s); await _ctx.SaveChangesAsync(); } }
        public async Task RemoveStudentAsync(Guid gid, Guid sid) { var g = await _db.Include(x => x.Students).FirstOrDefaultAsync(x => x.Id == gid); var s = g?.Students.FirstOrDefault(x => x.Id == sid); if (g != null && s != null) { g.Students.Remove(s); await _ctx.SaveChangesAsync(); } }
        public async Task AssignSubjectAsync(Guid gid, Guid sid) { var g = await _db.Include(x => x.Subjects).FirstOrDefaultAsync(x => x.Id == gid); var s = await _ctx.Subjects.FindAsync(sid); if (g != null && s != null) { g.Subjects.Add(s); await _ctx.SaveChangesAsync(); } }
        public async Task AssignTeacherAsync(Guid gid, Guid tid) { var g = await _db.Include(x => x.Teachers).FirstOrDefaultAsync(x => x.Id == gid); var t = await _ctx.Teachers.FindAsync(tid); if (g != null && t != null) { g.Teachers.Add(t); await _ctx.SaveChangesAsync(); } }
    }
