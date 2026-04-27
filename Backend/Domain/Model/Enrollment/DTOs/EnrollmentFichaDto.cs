using LuminiSchool.Domain.Entities.Guardian;
using LuminiSchool.Domain.Entities.Parent;
using LuminiSchool.Domain.Entities.Student;
using LuminiSchool.Domain.Model.Parent.DTOs;

namespace LuminiSchool.Domain.Model.Enrollment.DTOs
{
    // ── Entrada: Ficha completa de matrícula ──────────────────────────────────

    public class CreateFichaMatriculaDto
    {
        // 1. Datos del estudiante
        public StudentFichaDto Student { get; set; } = new();

        // 2. Padres (opcionales - puede venir uno, ambos o ninguno)
        public CreateParentDto? Father { get; set; }
        public CreateParentDto? Mother { get; set; }

        // 3. Acudiente (obligatorio)
        public CreateAcudienteDto Guardian { get; set; } = new();

        // 4. Datos de matrícula
        public Guid GradeId      { get; set; }
        public int  AcademicYear { get; set; }
    }

    public class StudentFichaDto
    {
        public DocumentType DocumentType        { get; set; } = DocumentType.TI;
        public string       DocumentNumber      { get; set; } = string.Empty;
        public string       FirstName           { get; set; } = string.Empty;
        public string       LastName            { get; set; } = string.Empty;
        public Gender       Gender              { get; set; }
        public DateTime     BirthDate           { get; set; }
        public string?      BirthPlace          { get; set; }
        public string?      Address             { get; set; }
        public string?      City                { get; set; }
        public string?      Phone               { get; set; }
        public string?      Email               { get; set; }
        public string?      Eps                 { get; set; }
        public BloodType?   BloodType           { get; set; }
        public string?      PreviousInstitution { get; set; }
    }

    public class CreateAcudienteDto
    {
        /// <summary>
        /// Father → reutiliza datos del padre registrado (no duplica).
        /// Mother → reutiliza datos de la madre registrada (no duplica).
        /// Other  → persona distinta, requiere sus propios datos.
        /// </summary>
        public GuardianRelationship GuardianType { get; set; } = GuardianRelationship.Other;

        // Solo requerido si GuardianType = Other
        public string?      FullName       { get; set; }
        public DocumentType? DocumentType  { get; set; }
        public string?      DocumentNumber { get; set; }
        public string?      Phone          { get; set; }
        public string?      Address        { get; set; }
        public string?      Email          { get; set; }
        public string?      Occupation     { get; set; }
    }

    // ── Salida ────────────────────────────────────────────────────────────────

    public class FichaMatriculaResponseDto
    {
        public Guid         EnrollmentId   { get; set; }
        public string       StudentName    { get; set; } = string.Empty;
        public string       DocumentNumber { get; set; } = string.Empty;
        public string       GradeName      { get; set; } = string.Empty;
        public int          AcademicYear   { get; set; }
        public string       GuardianName   { get; set; } = string.Empty;
        public string       GuardianPhone  { get; set; } = string.Empty;
        public ParentDto?   Father         { get; set; }
        public ParentDto?   Mother         { get; set; }
        public DateTime     EnrollmentDate { get; set; }
    }
}
