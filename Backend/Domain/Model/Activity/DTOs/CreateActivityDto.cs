using LuminiSchool.Domain.Entities.Activity;
using System;

namespace LuminiSchool.Domain.Model.Activity.DTOs
{
    public class CreateActivityDto
    {
        // Nullable: el controlador lo resuelve desde el JWT.
        // SuperAdmin/Admin/Rector sin perfil de docente dejan esto en null.
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
    }
}
