
namespace LuminiSchool.Domain.Model.School.DTOs
{
    public class SchoolDto
    {
        public Guid   Id          { get; set; }
        public string SchoolName  { get; set; } = string.Empty;
        public string SchoolNit   { get; set; } = string.Empty;
        public string City        { get; set; } = string.Empty;
        public string Department  { get; set; } = string.Empty;
        public string Phone       { get; set; } = string.Empty;
        public string Email       { get; set; } = string.Empty;
        public string Address     { get; set; } = string.Empty;
        public string? LogoUrl    { get; set; }

        public double MinGrade     { get; set; }
        public double MaxGrade     { get; set; }
        public double PassingGrade { get; set; }
        public int    NumPeriods   { get; set; }
        public int    CurrentYear  { get; set; }

        public Dictionary<string, bool> Modules { get; set; } = new();
        public Dictionary<string, bool> Roles   { get; set; } = new();

        public bool     IsActive  { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
