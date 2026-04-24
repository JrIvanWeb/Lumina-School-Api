using LuminiSchool.Domain.Entities.Certificate;
using LuminiSchool.Domain.Model.Certificate.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Business.Services.Contract
{
    public interface ICertificateService
    {
        Task<IEnumerable<CertificateDto>> GetByStudentAsync(Guid studentId);
        Task<byte[]> GeneratePdfAsync(Guid studentId, CertificateType type);
    }
}
