namespace LuminiSchool.Domain.Entities.Teacher
{
    public class TeacherEntity
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName  { get; set; } = string.Empty;
        public string DocumentNumber { get; set; } = string.Empty;
        public string? Email     { get; set; }
        public string? Phone     { get; set; }
        public string? Specialty { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt  { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        public ICollection<Subject.SubjectEntity> Subjects { get; set; } = new List<Subject.SubjectEntity>();
    }
}
