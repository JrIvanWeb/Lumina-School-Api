using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Domain.Entities.Schedule
{
    public class ScheduleEntity
    {
        public Guid Id { get; set; }
        public Guid GradeId { get; set; }
        public Guid SubjectId { get; set; }
        public Guid TeacherId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string? Classroom { get; set; }
        public bool IsActive { get; set; } = true;
        public Grade.GradeEntity? Grade { get; set; }
        public Subject.SubjectEntity? Subject { get; set; }
        public Teacher.TeacherEntity? Teacher { get; set; }
    }
}
