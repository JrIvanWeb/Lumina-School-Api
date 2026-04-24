using LuminiSchool.Domain.Entities.Certificate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Infrastructure.Repositories.Contract
{
    public interface ICertificateRepository : IGenericRepository<CertificateEntity>
    {
        Task<IEnumerable<CertificateEntity>> GetByStudentAsync(Guid studentId);
    }
}
