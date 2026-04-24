using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Domain.Model.Attendance.DTOs
{
    public class AttendanceReportDto { 
        public Guid StudentId { get; set; } 
        public int TotalDays { get; set; } 
        public int Present { get; set; } 
        public int Absent { get; set; } 
        public int Late { get; set; } 
        public int Excused { get; set; } 
        public decimal AttendancePercentage { get; set; } 
    }
}
