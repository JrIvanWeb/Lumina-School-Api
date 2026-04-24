using AutoMapper;
using LuminiSchool.Business.Services.Contract;
using LuminiSchool.Domain.Entities.Bulletin;
using LuminiSchool.Domain.Model.Bulletin.DTOs;
using LuminiSchool.Infrastructure.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Business.Services.Implementation
{
    public class BulletinService : IBulletinService
    {
        private readonly IBulletinRepository _r; private readonly IGradeRecordRepository _gr; private readonly IMapper _m;
        public BulletinService(IBulletinRepository r, IGradeRecordRepository gr, IMapper m) { _r = r; _gr = gr; _m = m; }
        public async Task<IEnumerable<BulletinDto>> GetByStudentAsync(Guid sid) => _m.Map<IEnumerable<BulletinDto>>(await _r.GetByStudentAsync(sid));
        public async Task<BulletinDto> GenerateAsync(Guid sid, Guid pid) { var avg = await _gr.GetAverageByStudentAndPeriodAsync(sid, pid) ?? 0; var ex = await _r.GetByStudentAndPeriodAsync(sid, pid); if (ex != null) { ex.GeneralAverage = avg; ex.GeneratedAt = DateTime.UtcNow; await _r.UpdateAsync(ex); return _m.Map<BulletinDto>(ex); } var e = new BulletinEntity { Id = Guid.NewGuid(), StudentId = sid, AcademicPeriodId = pid, GeneralAverage = avg, GeneratedAt = DateTime.UtcNow }; return _m.Map<BulletinDto>(await _r.AddAsync(e)); }
        public Task<byte[]> ExportPdfAsync(Guid sid, Guid pid) => Task.FromResult(Array.Empty<byte>());
    }
}
