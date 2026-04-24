using LuminiSchool.Domain.Entities.AcademicPeriod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Infrastructure.Repositories.Contract
{
    public interface IAcademicPeriodRepository : IGenericRepository<AcademicPeriodEntity>
    {
        Task<AcademicPeriodEntity?> GetActiveAsync();
        Task<IEnumerable<AcademicPeriodEntity>> GetByYearAsync(int year);
    }
}
