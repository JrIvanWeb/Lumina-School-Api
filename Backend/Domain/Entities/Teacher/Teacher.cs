namespace LuminiSchool.Domain.Entities.Teacher
{
    public class TeacherEntity
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }

        // Datos de cuenta
        public string  FirstName { get; set; } = string.Empty;
        public string  LastName  { get; set; } = string.Empty;
        public string? Email     { get; set; }

        // Datos personales
        public string  DocumentType   { get; set; } = "CC";
        public string  DocumentNumber { get; set; } = string.Empty;
        public string? Phone          { get; set; }
        public DateTime? BirthDate    { get; set; }
        public string  Gender         { get; set; } = "M";
        public string? Address        { get; set; }
        public string? City           { get; set; }

        // Datos profesionales
        public string? Profession              { get; set; }
        public string? Specialization          { get; set; }
        public string  EducationLevel          { get; set; } = "Pregrado";
        public int     TeachingExperienceYears { get; set; }
        public string  ContractType            { get; set; } = "Planta";
        public DateTime? HireDate              { get; set; }
        public string? Bio                     { get; set; }

        // Auditoría
        public bool      IsActive  { get; set; } = true;
        public DateTime  CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        public ICollection<Subject.SubjectEntity> Subjects { get; set; } = new List<Subject.SubjectEntity>();
    }
}
