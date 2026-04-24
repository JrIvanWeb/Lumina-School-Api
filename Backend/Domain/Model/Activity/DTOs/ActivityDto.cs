using LuminiSchool.Domain.Entities.Activity;

namespace LuminiSchool.Domain.Model.Activity.DTOs
{
    public class ActivityDto { 
        public Guid Id { get; set; } 
        public Guid TeacherId { get; set; } 
        public Guid SubjectId { get; set; } 
        public string SubjectName { get; set; } = string.Empty; 
        public Guid GradeId { get; set; } 
        public Guid AcademicPeriodId { get; set; } 
        public string Title { get; set; } = string.Empty; 
        public string Description { get; set; } = string.Empty; 
        public ActivityType Type { get; set; } 
        public decimal MaxScore { get; set; } 
        public DateTime DueDate { get; set; } 
    }
}