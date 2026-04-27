using LuminiSchool.Domain.Entities.Achievement;
using LuminiSchool.Infrastructure.Repositories.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Infrastructure.Repositories.Implementation
{
    public class AchievementRepository : GenericRepository<AchievementEntity>, IAchievementRepository
    {
        public AchievementRepository(ApplicationDbContext ctx) : base(ctx) { }
        public async Task<IEnumerable<AchievementEntity>> GetBySubjectAsync(Guid sid) => await _db.Where(a => a.SubjectId == sid).ToListAsync();
    }
}
