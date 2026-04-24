using LuminiSchool.Domain.Entities.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Infrastructure.Repositories.Contract
{
    public interface IReportRepository : IGenericRepository<ReportEntity>
    {
        Task<IEnumerable<ReportEntity>> GetByTypeAsync(ReportType type);
    }
}
