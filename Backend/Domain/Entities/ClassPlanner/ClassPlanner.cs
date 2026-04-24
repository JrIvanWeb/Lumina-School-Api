using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Domain.Entities.ClassPlanner
{
    public class ClassPlannerEntity
    {
        public Guid Id { get; set; }
        public Guid TeacherId { get; set; }
        public Guid SubjectId { get; set; }
        public Guid GradeId { get; set; }
        public Guid AcademicPeriodId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Objectives { get; set; } = string.Empty;
        public string Contents { get; set; } = string.Empty;
        public string? Resources { get; set; }
        public DateTime PlannedDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Teacher.TeacherEntity? Teacher { get; set; }
        public Subject.SubjectEntity? Subject { get; set; }
        public Grade.GradeEntity? Grade { get; set; }
        public AcademicPeriod.AcademicPeriodEntity? AcademicPeriod { get; set; }
    }
}