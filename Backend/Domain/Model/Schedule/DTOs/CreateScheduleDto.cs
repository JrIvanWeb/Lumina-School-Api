using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Domain.Model.Schedule.DTOs
{
    public class CreateScheduleDto { 
        public Guid GradeId { get; set; } 
        public Guid SubjectId { get; set; } 
        public Guid TeacherId { get; set; } 
        public DayOfWeek DayOfWeek { get; set; } 
        public TimeSpan StartTime { get; set; } 
        public TimeSpan EndTime { get; set; } 
        public string? Classroom { get; set; } 
    }
}
