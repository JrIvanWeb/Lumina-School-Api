using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Domain.Entities.DiagnosticTest
{
    public enum DiagnosticStatus { Draft, Active, Completed }

    public class DiagnosticTestEntity
    {
        public Guid Id { get; set; }
        public Guid TeacherId { get; set; }
        public Guid GradeId { get; set; }
        public Guid SubjectId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DiagnosticStatus Status { get; set; } = DiagnosticStatus.Draft;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Teacher.TeacherEntity? Teacher { get; set; }
        public Grade.GradeEntity? Grade { get; set; }
        public Subject.SubjectEntity? Subject { get; set; }
        public ICollection<DiagnosticResultEntity> Results { get; set; } = new List<DiagnosticResultEntity>();
    }

    public class DiagnosticResultEntity
    {
        public Guid Id { get; set; }
        public Guid DiagnosticTestId { get; set; }
        public Guid StudentId { get; set; }
        public decimal Score { get; set; }
        public string? Observations { get; set; }
        public DateTime TakenAt { get; set; } = DateTime.UtcNow;
        public DiagnosticTestEntity? DiagnosticTest { get; set; }
        public Student.StudentEntity? Student { get; set; }
    }
}
