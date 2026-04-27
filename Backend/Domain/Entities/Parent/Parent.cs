using LuminiSchool.Domain.Entities.Student;

namespace LuminiSchool.Domain.Entities.Parent
{
    public enum ParentRole { Father, Mother }

    public class ParentEntity
    {
        public Guid       Id             { get; set; } = Guid.NewGuid();
        public string     FullName       { get; set; } = string.Empty;
        public DocumentType DocumentType { get; set; } = DocumentType.CC;
        public string     DocumentNumber { get; set; } = string.Empty;
        public string?    Phone          { get; set; }
        public string?    Occupation     { get; set; }
        public string?    Email          { get; set; }
        public ParentRole Role           { get; set; }   // Father o Mother
        public bool       IsActive       { get; set; } = true;
        public DateTime   CreatedAt      { get; set; } = DateTime.UtcNow;

        // ── Navegación ───────────────────────────────────────────────────────
        public ICollection<StudentEntity> Students { get; set; } = new List<StudentEntity>();
    }
}
