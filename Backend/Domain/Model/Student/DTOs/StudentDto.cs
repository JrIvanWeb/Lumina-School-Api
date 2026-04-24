using LuminiSchool.Domain.Entities.Student;

namespace LuminiSchool.Domain.Model.Student.DTOs
{
    public class StudentDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName => $"{FirstName} {LastName}";
        public string DocumentNumber { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public StudentStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
