namespace LuminiSchool.Domain.Entities.Grade
{
    public class GradeEntity
    {
        public Guid Id { get; set; }
        public string  Name    { get; set; } = string.Empty;
        public string  Level   { get; set; } = string.Empty;
        public string? Section { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Student.StudentEntity>  Students { get; set; } = new List<Student.StudentEntity>();
        public ICollection<Subject.SubjectEntity>  Subjects { get; set; } = new List<Subject.SubjectEntity>();
        public ICollection<Teacher.TeacherEntity>  Teachers { get; set; } = new List<Teacher.TeacherEntity>();
    }
}
