using LuminiSchool.Domain.Entities.SchoolRepresentative;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Infrastructure.Repositories.Contract
{
    public interface ISchoolRepresentativeRepository : IGenericRepository<SchoolRepresentativeEntity>
    {
        Task<IEnumerable<SchoolRepresentativeEntity>> GetByYearAsync(int year);
        Task<IEnumerable<SchoolRepresentativeEntity>> GetActiveAsync();
    }
}
