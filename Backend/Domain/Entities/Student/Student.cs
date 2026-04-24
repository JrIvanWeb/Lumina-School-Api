namespace LuminiSchool.Domain.Entities.Student
{
    public enum StudentStatus { Active, Retired, Graduated, Suspended }

    public class StudentEntity
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName  { get; set; } = string.Empty;
        public string DocumentNumber { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string? Email   { get; set; }
        public string? Phone   { get; set; }
        public string? Address { get; set; }
        public StudentStatus Status { get; set; } = StudentStatus.Active;
        public DateTime CreatedAt  { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        public ICollection<Guardian.GuardianEntity>  Guardians  { get; set; } = new List<Guardian.GuardianEntity>();
        public ICollection<Grade.GradeEntity>        Grades     { get; set; } = new List<Grade.GradeEntity>();
        public ICollection<Enrollment.EnrollmentEntity> Enrollments { get; set; } = new List<Enrollment.EnrollmentEntity>();
    }
}
