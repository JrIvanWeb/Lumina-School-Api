using LuminiSchool.Domain.Entities.Attendance;

namespace LuminiSchool.Domain.Model.Attendance.DTOs
{
    public class AttendanceDto
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public Guid SubjectId { get; set; }
        public string? SubjectName { get; set; }
        public Guid GradeId { get; set; }
        public DateTime Date { get; set; }
        public AttendanceStatus Status { get; set; }
        public string? Notes { get; set; }
    }
}
