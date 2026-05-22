using LuminiSchool.Domain.Entities.Activity;
using System;

namespace LuminiSchool.Domain.Model.Activity.DTOs
{
    public class ActivityDto
    {
        public Guid Id { get; set; }
        public Guid TeacherId { get; set; }
        public Guid SubjectId { get; set; }
        public string SubjectName { get; set; } = string.Empty;
        public Guid GradeId { get; set; }
        public Guid AcademicPeriodId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Objective { get; set; } = string.Empty;
        public string Standard { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Resources { get; set; } = string.Empty;
        public ActivityType Type { get; set; }
        public ActivityStatus Status { get; set; }
        public decimal MaxScore { get; set; }
        public bool HasDueDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
