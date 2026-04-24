namespace LuminiSchool.Domain.Model.Activity.DTOs
{
    public class ActivitySubmissionDto { 
        public Guid Id { get; set; } 
        public Guid ActivityId { get; set; } 
        public Guid StudentId { get; set; } 
        public string StudentName { get; set; } = string.Empty; 
        public string? FileUrl { get; set; } 
        public string? Comments { get; set; } 
        public decimal? Score { get; set; } 
        public string? Feedback { get; set; } 
        public DateTime SubmittedAt { get; set; } 
    }
}
