using LuminiSchool.Domain.Entities.Observer;
using LuminiSchool.Infrastructure.Repositories.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Infrastructure.Repositories.Implementation
{
    public class ObserverRepository : GenericRepository<ObserverEntity>, IObserverRepository
    {
        public ObserverRepository(ApplicationDbContext ctx) : base(ctx) { }
        public async Task<IEnumerable<ObserverEntity>> GetByStudentAsync(Guid sid) => await _db.Where(o => o.StudentId == sid).Include(o => o.Teacher).OrderByDescending(o => o.Date).ToListAsync();
    }
}
