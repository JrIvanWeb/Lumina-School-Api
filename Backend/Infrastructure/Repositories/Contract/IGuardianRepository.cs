using LuminiSchool.Domain.Entities.Guardian;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Infrastructure.Repositories.Contract
{
    public interface IGuardianRepository : IGenericRepository<GuardianEntity>
    {
        Task<IEnumerable<GuardianEntity>> GetByStudentAsync(Guid studentId);
        Task AssignStudentAsync(Guid guardianId, Guid studentId);
    }
}
