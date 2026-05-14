namespace LuminiSchool.Domain.Model.Teacher.DTOs
{
    public class UpdateTeacherDto
    {
        public string? FirstName { get; set; }
        public string? LastName  { get; set; }
        public string? Email     { get; set; }

        public string? DocumentType   { get; set; }
        public string? DocumentNumber { get; set; }
        public string? Phone          { get; set; }
        public string? BirthDate      { get; set; }
        public string? Gender         { get; set; }
        public string? Address        { get; set; }
        public string? City           { get; set; }

        public string? Profession              { get; set; }
        public string? Specialization          { get; set; }
        public string? EducationLevel          { get; set; }
        public int?    TeachingExperienceYears { get; set; }
        public string? ContractType            { get; set; }
        public string? HireDate                { get; set; }
        public string? Bio                     { get; set; }
    }
}
