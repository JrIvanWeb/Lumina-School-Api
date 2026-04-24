using LuminiSchool.Domain.Entities.Report;
using LuminiSchool.Infrastructure.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Infrastructure.Repositories.Implementation
{
    public class ReportRepository : GenericRepository<ReportEntity>, IReportRepository
    {
        public ReportRepository(ApplicationDbContext ctx) : base(ctx) { }
        public async Task<IEnumerable<ReportEntity>> GetByTypeAsync(ReportType type) => await _db.Where(r => r.Type == type).ToListAsync();
    }
}
