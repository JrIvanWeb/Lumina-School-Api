namespace LuminiSchool.Domain.Model.Teacher.DTOs
{
    public class TeacherDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName => $"{FirstName} {LastName}";
        public string DocumentNumber { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Specialty { get; set; }
        public bool IsActive { get; set; }
    }
}
