using AutoMapper;
using LuminiSchool.Business.Services.Contract;
using LuminiSchool.Domain.Entities.Attendance;
using LuminiSchool.Domain.Model.Attendance.DTOs;
using LuminiSchool.Infrastructure.Repositories.Contract;


namespace LuminiSchool.Business.Services.Implementation
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepository _r; private readonly IMapper _m;
        public AttendanceService(IAttendanceRepository r, IMapper m) { _r = r; _m = m; }
        public async Task<IEnumerable<AttendanceDto>> GetByStudentAsync(Guid sid) => _m.Map<IEnumerable<AttendanceDto>>(await _r.GetByStudentAsync(sid));
        public async Task<IEnumerable<AttendanceDto>> GetByDateAndGradeAsync(DateTime date, Guid gid) => _m.Map<IEnumerable<AttendanceDto>>(await _r.GetByDateAndGradeAsync(date, gid));
        public async Task<AttendanceDto> RegisterAsync(CreateAttendanceDto dto) { var e = _m.Map<AttendanceEntity>(dto); e.Id = Guid.NewGuid(); return _m.Map<AttendanceDto>(await _r.AddAsync(e)); }
        public async Task<AttendanceReportDto> GetReportAsync(Guid sid, DateTime from, DateTime to) { var r = (await _r.GetByStudentAndPeriodAsync(sid, from, to)).ToList(); int total = r.Count, present = r.Count(x => x.Status == AttendanceStatus.Present); return new AttendanceReportDto { StudentId = sid, TotalDays = total, Present = present, Absent = r.Count(x => x.Status == AttendanceStatus.Absent), Late = r.Count(x => x.Status == AttendanceStatus.Late), Excused = r.Count(x => x.Status == AttendanceStatus.Excused), AttendancePercentage = total > 0 ? Math.Round((decimal)present / total * 100, 2) : 0 }; }
    }
}
