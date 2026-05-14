namespace LuminiSchool.Domain.Model.Subject.DTOs
{
    public class UpdateSubjectDto
    {
        public string  Name        { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int     WeeklyHours { get; set; }
        public bool    IsActive    { get; set; }
    }
}
