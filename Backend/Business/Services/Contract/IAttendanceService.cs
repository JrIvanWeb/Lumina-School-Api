using LuminiSchool.Domain.Model.Attendance.DTOs;

namespace LuminiSchool.Business.Services.Contract
{
    public interface IAttendanceService
    {
        Task<IEnumerable<AttendanceDto>> GetByStudentAsync(Guid studentId);
        Task<IEnumerable<AttendanceDto>> GetByDateAndGradeAsync(DateTime date, Guid gradeId);
        Task<AttendanceDto> RegisterAsync(CreateAttendanceDto dto);
        Task<AttendanceDto> UpdateAsync(Guid id, UpdateAttendanceDto dto);
        Task DeleteAsync(Guid id);
        Task<AttendanceReportDto> GetReportAsync(Guid studentId, DateTime from, DateTime to);
    }
}
