namespace LuminiSchool.Domain.Model.Enrollment.DTOs
{
    /// <summary>Matrícula simple (estudiante ya existente).</summary>
    public class CreateEnrollmentDto
    {
        public Guid StudentId    { get; set; }
        public Guid GradeId      { get; set; }
        public Guid GuardianId   { get; set; }
        public int  AcademicYear { get; set; }
    }
}
