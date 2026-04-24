using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Domain.Entities.Activity
{
    public enum ActivityType { Task, Exam, Quiz, Project, Lab }

    public class ActivityEntity
    {
        public Guid Id { get; set; }
        public Guid TeacherId { get; set; }
        public Guid SubjectId { get; set; }
        public Guid GradeId { get; set; }
        public Guid AcademicPeriodId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ActivityType Type { get; set; }
        public decimal MaxScore { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Teacher.TeacherEntity? Teacher { get; set; }
        public Subject.SubjectEntity? Subject { get; set; }
        public ICollection<ActivitySubmissionEntity> Submissions { get; set; } = new List<ActivitySubmissionEntity>();
    }

    public class ActivitySubmissionEntity
    {
        public Guid Id { get; set; }
        public Guid ActivityId { get; set; }
        public Guid StudentId { get; set; }
        public string? FileUrl { get; set; }
        public string? Comments { get; set; }
        public decimal? Score { get; set; }
        public string? Feedback { get; set; }
        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
        public DateTime? GradedAt { get; set; }
        public ActivityEntity? Activity { get; set; }
        public Student.StudentEntity? Student { get; set; }
    }
}