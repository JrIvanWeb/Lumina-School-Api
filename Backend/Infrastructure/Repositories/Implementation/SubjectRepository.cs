using LuminiSchool.Domain.Entities.Subject;
using LuminiSchool.Infrastructure.Repositories.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Infrastructure.Repositories.Implementation
{
    public class SubjectRepository : GenericRepository<SubjectEntity>, ISubjectRepository
    {
        public SubjectRepository(ApplicationDbContext ctx) : base(ctx) { }
        public async Task<IEnumerable<SubjectEntity>> GetByTeacherAsync(Guid tid) => await _db.Where(s => s.Teachers.Any(t => t.Id == tid)).ToListAsync();
    }
}
