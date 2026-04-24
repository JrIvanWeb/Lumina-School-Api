using LuminiSchool.Domain.Model.Bulletin.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Business.Services.Contract
{
    public interface IBulletinService
    {
        Task<IEnumerable<BulletinDto>> GetByStudentAsync(Guid studentId);
        Task<BulletinDto> GenerateAsync(Guid studentId, Guid periodId);
        Task<byte[]> ExportPdfAsync(Guid studentId, Guid periodId);
    }
}
