using System;
using System.Collections.Generic;

namespace LuminiSchool.Domain.Entities.Activity
{
    public enum ActivityType { Task, Exam, Quiz, Project, Lab }
    public enum ActivityStatus { Borrador, Publicado }

    public class ActivityEntity
    {
        public Guid Id { get; set; }

        // Nullable: SuperAdmin/Admin/Rector pueden crear actividades
        // sin tener un TeacherEntity asociado en la BD.
        public Guid? TeacherId { get; set; }

        public Guid SubjectId { get; set; }
        public Guid GradeId { get; set; }
        public Guid AcademicPeriodId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Objective { get; set; } = string.Empty;
        public string Standard { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Resources { get; set; } = string.Empty;
        public ActivityType Type { get; set; }
        public ActivityStatus Status { get; set; } = ActivityStatus.Borrador;
        public decimal MaxScore { get; set; }
        public bool HasDueDate { get; set; } = false;
        public DateTime DueDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navegación
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

        // Navegación
        public ActivityEntity? Activity { get; set; }
        public Student.StudentEntity? Student { get; set; }
    }
}
