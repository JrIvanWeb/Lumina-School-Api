namespace LuminiSchool.Domain.Model.School.DTOs

{
    public class CreateSchoolDto
    {
        public string SchoolName  { get; set; } = string.Empty;
        public string SchoolNit   { get; set; } = string.Empty;
        public string City        { get; set; } = string.Empty;
        public string Department  { get; set; } = string.Empty;
        public string Phone       { get; set; } = string.Empty;
        public string Email       { get; set; } = string.Empty;
        public string Address     { get; set; } = string.Empty;
        public string? LogoUrl    { get; set; }

        public double MinGrade     { get; set; } = 1;
        public double MaxGrade     { get; set; } = 5;
        public double PassingGrade { get; set; } = 3;
        public int    NumPeriods   { get; set; } = 4;
        public int    CurrentYear  { get; set; } = DateTime.UtcNow.Year;

        public Dictionary<string, bool> Modules { get; set; } = new();
        public Dictionary<string, bool> Roles   { get; set; } = new();
    }
}
