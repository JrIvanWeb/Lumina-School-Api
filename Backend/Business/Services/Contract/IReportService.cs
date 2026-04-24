using LuminiSchool.Domain.Model.Report.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Business.Services.Contract
{
    public interface IReportService
    {
        Task<IEnumerable<ReportDto>> GetAllAsync();
        Task<ReportDto> GenerateAsync(CreateReportDto dto, Guid userId);
    }
}
