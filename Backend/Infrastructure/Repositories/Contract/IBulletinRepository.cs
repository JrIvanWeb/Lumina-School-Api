using LuminiSchool.Domain.Entities.Bulletin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Infrastructure.Repositories.Contract
{
    public interface IBulletinRepository : IGenericRepository<BulletinEntity>
    {
        Task<IEnumerable<BulletinEntity>> GetByStudentAsync(Guid studentId);
        Task<BulletinEntity?> GetByStudentAndPeriodAsync(Guid studentId, Guid periodId);
    }
}
