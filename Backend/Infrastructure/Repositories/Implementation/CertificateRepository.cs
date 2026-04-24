using LuminiSchool.Domain.Entities.Certificate;
using LuminiSchool.Infrastructure.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Infrastructure.Repositories.Implementation
{
    public class CertificateRepository : GenericRepository<CertificateEntity>, ICertificateRepository
    {
        public CertificateRepository(ApplicationDbContext ctx) : base(ctx) { }
        public async Task<IEnumerable<CertificateEntity>> GetByStudentAsync(Guid sid) => await _db.Where(c => c.StudentId == sid).ToListAsync();
    }
}
