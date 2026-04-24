using System;
namespace LuminiSchool.Domain.Entities.SchoolRepresentative
{
    public class SchoolRepresentativeEntity
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public string Position { get; set; } = string.Empty;
        public int AcademicYear { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Student.StudentEntity? Student { get; set; }
    }
}