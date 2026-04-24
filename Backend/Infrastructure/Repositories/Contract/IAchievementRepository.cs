using LuminiSchool.Domain.Entities.Achievement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Infrastructure.Repositories.Contract
{
    public interface IAchievementRepository : IGenericRepository<AchievementEntity>
    {
        Task<IEnumerable<AchievementEntity>> GetBySubjectAsync(Guid subjectId);
    }
}
