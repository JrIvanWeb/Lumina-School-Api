using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Domain.Entities.Attendance
{
    public enum AttendanceStatus { Present, Absent, Late, Excused }

    public class AttendanceEntity
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public Guid SubjectId { get; set; }
        public Guid GradeId { get; set; }
        public DateTime Date { get; set; }
        public AttendanceStatus Status { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Student.StudentEntity? Student { get; set; }
        public Subject.SubjectEntity? Subject { get; set; }
    }
}
