using LuminiSchool.Domain.Entities.SchoolRepresentative;
using LuminiSchool.Infrastructure.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Infrastructure.Repositories.Implementation
{
    public class SchoolRepresentativeRepository : GenericRepository<SchoolRepresentativeEntity>, ISchoolRepresentativeRepository
    {
        public SchoolRepresentativeRepository(ApplicationDbContext ctx) : base(ctx) { }
        public async Task<IEnumerable<SchoolRepresentativeEntity>> GetByYearAsync(int year) => await _db.Where(r => r.AcademicYear == year).Include(r => r.Student).ToListAsync();
        public async Task<IEnumerable<SchoolRepresentativeEntity>> GetActiveAsync() => await _db.Where(r => r.IsActive).Include(r => r.Student).ToListAsync();
    }
}
