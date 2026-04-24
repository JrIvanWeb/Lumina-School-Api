namespace LuminiSchool.Domain.Entities.Subject
{
    public class SubjectEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int WeeklyHours { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Teacher.TeacherEntity> Teachers { get; set; } = new List<Teacher.TeacherEntity>();
    }
}
