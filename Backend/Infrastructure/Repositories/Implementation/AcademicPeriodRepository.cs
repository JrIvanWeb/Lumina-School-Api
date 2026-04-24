using LuminiSchool.Domain.Entities.AcademicPeriod;
using LuminiSchool.Infrastructure.Repositories.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Infrastructure.Repositories.Implementation
{
    public class AcademicPeriodRepository : GenericRepository<AcademicPeriodEntity>, IAcademicPeriodRepository
    {
        public AcademicPeriodRepository(ApplicationDbContext ctx) : base(ctx) { }
        public async Task<AcademicPeriodEntity?> GetActiveAsync() => await _db.FirstOrDefaultAsync(p => p.IsActive);
        public async Task<IEnumerable<AcademicPeriodEntity>> GetByYearAsync(int year) => await _db.Where(p => p.AcademicYear == year).ToListAsync();
    }
}
