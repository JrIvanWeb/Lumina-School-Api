using LuminiSchool.Domain.Entities.Enrollment;
using LuminiSchool.Domain.Entities.Student;
using LuminiSchool.Domain.Entities.Guardian;
using LuminiSchool.Domain.Model.Parent.DTOs;

namespace LuminiSchool.Domain.Model.Enrollment.DTOs
{
    public class EnrollmentDto
    {
        public Guid             Id             { get; set; }
        public Guid             StudentId      { get; set; }
        public string           StudentName    { get; set; } = string.Empty;
        public Guid             GradeId        { get; set; }
        public string           GradeName      { get; set; } = string.Empty;
        public string           GuardianName   { get; set; } = string.Empty;
        public int              AcademicYear   { get; set; }
        public EnrollmentStatus Status         { get; set; }
        public DateTime         EnrollmentDate { get; set; }
        public StudentFichaDto?  Student        { get; set; }
        public ParentDto?        Father         { get; set; }
        public ParentDto?        Mother         { get; set; }
        public CreateAcudienteDto? Guardian     { get; set; }
    }
}
