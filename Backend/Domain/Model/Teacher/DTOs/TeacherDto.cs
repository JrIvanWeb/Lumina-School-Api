namespace LuminiSchool.Domain.Model.Teacher.DTOs
{
    public class TeacherDto
    {
        public Guid    Id       { get; set; }
        public Guid?   UserId   { get; set; }
        public string  FullName { get; set; } = string.Empty;
        public string? Email    { get; set; }
        public bool    IsActive { get; set; }
        public DateTime CreatedAt { get; set; }

        // Datos personales
        public string  DocumentType   { get; set; } = "CC";
        public string  DocumentNumber { get; set; } = string.Empty;
        public string? Phone          { get; set; }
        public string? BirthDate      { get; set; }
        public string  Gender         { get; set; } = "M";
        public string? Address        { get; set; }
        public string? City           { get; set; }

        // Datos profesionales
        public string? Profession              { get; set; }
        public string? Specialization          { get; set; }
        public string  EducationLevel          { get; set; } = "Pregrado";
        public int     TeachingExperienceYears { get; set; }
        public string  ContractType            { get; set; } = "Planta";
        public string? HireDate                { get; set; }
        public string? Bio                     { get; set; }
    }
}
