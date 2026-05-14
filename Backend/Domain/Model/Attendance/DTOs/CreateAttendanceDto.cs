using LuminiSchool.Domain.Entities.Attendance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Domain.Model.Attendance.DTOs
{
    public class CreateAttendanceDto { 
        public Guid StudentId { get; set; } 
        public Guid SubjectId { get; set; } 
        public Guid GradeId { get; set; } 
        public DateTime Date { get; set; } 
        public AttendanceStatus Status { get; set; } 
        public string? Notes { get; set; } 
    }
}

namespace LuminiSchool.Domain.Model.Attendance.DTOs
{
    public class UpdateAttendanceDto
    {
        public AttendanceStatus Status { get; set; }
        public string? Notes { get; set; }
        public DateTime Date { get; set; }
    }
}
