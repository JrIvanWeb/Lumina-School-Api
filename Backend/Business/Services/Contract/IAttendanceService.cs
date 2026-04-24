using LuminiSchool.Domain.Model.Attendance.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Business.Services.Contract
{
    public interface IAttendanceService
    {
        Task<IEnumerable<AttendanceDto>> GetByStudentAsync(Guid studentId);
        Task<IEnumerable<AttendanceDto>> GetByDateAndGradeAsync(DateTime date, Guid gradeId);
        Task<AttendanceDto> RegisterAsync(CreateAttendanceDto dto);
        Task<AttendanceReportDto> GetReportAsync(Guid studentId, DateTime from, DateTime to);
    }
}
