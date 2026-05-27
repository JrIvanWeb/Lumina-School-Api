// ============================================================
// RUTA: Backend/Domain/Entities/School/School.cs
// ============================================================
namespace LuminiSchool.Domain.Entities.School
{
    public class SchoolEntity
    {
        public Guid Id { get; set; }

        // Información general
        public string SchoolName  { get; set; } = string.Empty;
        public string SchoolNit   { get; set; } = string.Empty;
        public string City        { get; set; } = string.Empty;
        public string Department  { get; set; } = string.Empty;
        public string Phone       { get; set; } = string.Empty;
        public string Email       { get; set; } = string.Empty;
        public string Address     { get; set; } = string.Empty;
        public string? LogoUrl    { get; set; }

        // Configuración académica
        public double MinGrade     { get; set; } = 1;
        public double MaxGrade     { get; set; } = 5;
        public double PassingGrade { get; set; } = 3;
        public int    NumPeriods   { get; set; } = 4;
        public int    CurrentYear  { get; set; } = DateTime.UtcNow.Year;

        // Módulos habilitados (almacenados como JSON en la BD)
        public string ModulesJson { get; set; } = "{}";

        // Roles habilitados (almacenados como JSON en la BD)
        public string RolesJson { get; set; } = "{}";

        public bool     IsActive  { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
