using LuminiSchool.Domain.Entities.IcfesSimulator;
using LuminiSchool.Infrastructure.Repositories.Contract;
using Microsoft.EntityFrameworkCore;

namespace LuminiSchool.Infrastructure.Repositories.Implementation
{
    public class IcfesSimulatorRepository : GenericRepository<IcfesSimulatorEntity>, IIcfesSimulatorRepository
    {
        public IcfesSimulatorRepository(ApplicationDbContext ctx) : base(ctx) { }
        public async Task<IEnumerable<IcfesSimulatorEntity>> GetByStatusAsync(IcfesSimulatorStatus status) => await _db.Where(i => i.Status == status).ToListAsync();
        public async Task<IEnumerable<IcfesResultEntity>> GetResultsBySimulatorAsync(Guid sid) => await _ctx.IcfesResults.Where(r => r.IcfesSimulatorId == sid).Include(r => r.Student).ToListAsync();
        public async Task<IEnumerable<IcfesResultEntity>> GetResultsByStudentAsync(Guid sid) => await _ctx.IcfesResults.Where(r => r.StudentId == sid).ToListAsync();
        public async Task<IcfesResultEntity> AddResultAsync(IcfesResultEntity r) { await _ctx.IcfesResults.AddAsync(r); await _ctx.SaveChangesAsync(); return r; }
    }
}
