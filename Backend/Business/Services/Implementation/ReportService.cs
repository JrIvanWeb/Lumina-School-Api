using AutoMapper;
using LuminiSchool.Business.Services.Contract;
using LuminiSchool.Domain.Entities.Report;
using LuminiSchool.Domain.Model.Report.DTOs;
using LuminiSchool.Infrastructure.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Business.Services.Implementation
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _r; private readonly IMapper _m;
        public ReportService(IReportRepository r, IMapper m) { _r = r; _m = m; }
        public async Task<IEnumerable<ReportDto>> GetAllAsync() => _m.Map<IEnumerable<ReportDto>>(await _r.GetAllAsync());
        public async Task<ReportDto> GenerateAsync(CreateReportDto dto, Guid uid) { var e = _m.Map<ReportEntity>(dto); e.Id = Guid.NewGuid(); e.GeneratedByUserId = uid; e.GeneratedAt = DateTime.UtcNow; return _m.Map<ReportDto>(await _r.AddAsync(e)); }
    }
}
