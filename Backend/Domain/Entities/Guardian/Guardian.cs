namespace LuminiSchool.Domain.Entities.Guardian
{
    public class GuardianEntity
    {
        public Guid Id { get; set; }
        public string FirstName     { get; set; } = string.Empty;
        public string LastName      { get; set; } = string.Empty;
        public string DocumentNumber{ get; set; } = string.Empty;
        public string Relationship  { get; set; } = string.Empty;
        public string? Email  { get; set; }
        public string  Phone  { get; set; } = string.Empty;
        public string? Address{ get; set; }
        public bool IsActive  { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Student.StudentEntity> Students { get; set; } = new List<Student.StudentEntity>();
    }
}
