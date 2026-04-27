using LuminiSchool.Domain.Entities.Student;

namespace LuminiSchool.Domain.Entities.Guardian
{
    public enum GuardianRelationship { Father, Mother, Other }

    public class GuardianEntity
    {
        public Guid   Id             { get; set; } = Guid.NewGuid();
        public string FullName       { get; set; } = string.Empty;
        public DocumentType DocumentType   { get; set; } = DocumentType.CC;
        public string DocumentNumber { get; set; } = string.Empty;
        public string Phone          { get; set; } = string.Empty;
        public string? Address       { get; set; }
        public string? Email         { get; set; }
        public string? Occupation    { get; set; }

        /// <summary>
        /// Father → es el padre registrado.
        /// Mother → es la madre registrada.
        /// Other  → es otra persona; sus datos son propios, no reutilizados.
        /// </summary>
        public GuardianRelationship Relationship { get; set; } = GuardianRelationship.Other;

        /// <summary>
        /// Si Relationship es Father o Mother, apunta al ParentEntity correspondiente
        /// para evitar duplicar información.
        /// </summary>
        public Guid? ParentId { get; set; }
        public Parent.ParentEntity? Parent { get; set; }

        public bool     IsActive  { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // ── Navegación ───────────────────────────────────────────────────────
        public ICollection<StudentEntity> Students { get; set; } = new List<StudentEntity>();
    }
}
