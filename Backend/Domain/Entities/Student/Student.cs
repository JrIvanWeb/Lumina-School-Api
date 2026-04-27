namespace LuminiSchool.Domain.Entities.Student
{
    public enum StudentStatus { Active, Retired, Graduated, Suspended }
    public enum DocumentType  { CC, TI, CE, RC, Passport }
    public enum Gender        { Male, Female, Other }
    public enum BloodType     { APos, ANeg, BPos, BNeg, OPos, ONeg, ABPos, ABNeg }

    public class StudentEntity
    {
        public Guid   Id             { get; set; } = Guid.NewGuid();

        // ── Documento ────────────────────────────────────────────────────────
        public DocumentType DocumentType   { get; set; } = DocumentType.TI;
        public string       DocumentNumber { get; set; } = string.Empty;

        // ── Nombres ──────────────────────────────────────────────────────────
        public string FirstName { get; set; } = string.Empty;
        public string LastName  { get; set; } = string.Empty;
        public Gender Gender    { get; set; } = Gender.Male;

        // ── Nacimiento ───────────────────────────────────────────────────────
        public DateTime BirthDate  { get; set; }
        public string?  BirthPlace { get; set; }

        // ── Contacto ─────────────────────────────────────────────────────────
        public string? Address { get; set; }
        public string? City    { get; set; }
        public string? Phone   { get; set; }
        public string? Email   { get; set; }

        // ── Salud ────────────────────────────────────────────────────────────
        public string?    Eps       { get; set; }
        public BloodType? BloodType { get; set; }

        // ── Académico ────────────────────────────────────────────────────────
        public string? PreviousInstitution { get; set; }   // Institución de procedencia (opcional)

        // ── Estado ───────────────────────────────────────────────────────────
        public StudentStatus Status    { get; set; } = StudentStatus.Active;
        public DateTime      CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime?     UpdatedAt { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────
        public ICollection<Guardian.GuardianEntity>     Guardians   { get; set; } = new List<Guardian.GuardianEntity>();
        public ICollection<Grade.GradeEntity>           Grades      { get; set; } = new List<Grade.GradeEntity>();
        public ICollection<Enrollment.EnrollmentEntity> Enrollments { get; set; } = new List<Enrollment.EnrollmentEntity>();
        public ICollection<Parent.ParentEntity>         Parents     { get; set; } = new List<Parent.ParentEntity>();
    }
}
