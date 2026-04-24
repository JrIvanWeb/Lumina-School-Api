using LuminiSchool.Domain.Entities.DiagnosticTest;
using LuminiSchool.Infrastructure.Repositories.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Infrastructure.Repositories.Implementation
{
    public class DiagnosticTestRepository : GenericRepository<DiagnosticTestEntity>, IDiagnosticTestRepository
    {
        public DiagnosticTestRepository(ApplicationDbContext ctx) : base(ctx) { }
        public async Task<IEnumerable<DiagnosticTestEntity>> GetByTeacherAsync(Guid tid) => await _db.Where(d => d.TeacherId == tid).ToListAsync();
        public async Task<IEnumerable<DiagnosticTestEntity>> GetByGradeAsync(Guid gid) => await _db.Where(d => d.GradeId == gid).ToListAsync();
        public async Task<IEnumerable<DiagnosticResultEntity>> GetResultsAsync(Guid tid) => await _ctx.DiagnosticResults.Where(r => r.DiagnosticTestId == tid).Include(r => r.Student).ToListAsync();
        public async Task<DiagnosticResultEntity> AddResultAsync(DiagnosticResultEntity r) { await _ctx.DiagnosticResults.AddAsync(r); await _ctx.SaveChangesAsync(); return r; }
    }
}
