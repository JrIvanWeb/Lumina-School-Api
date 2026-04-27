namespace LuminiSchool.Domain.Entities.Enrollment
{
    public enum EnrollmentStatus { Active, Withdrawn, Graduated, Transferred }

    public class EnrollmentEntity
    {
        public Guid Id           { get; set; } = Guid.NewGuid();
        public Guid StudentId    { get; set; }
        public Guid GradeId      { get; set; }
        public Guid GuardianId   { get; set; }   // Acudiente obligatorio
        public int  AcademicYear { get; set; }
        public EnrollmentStatus Status         { get; set; } = EnrollmentStatus.Active;
        public DateTime         EnrollmentDate { get; set; } = DateTime.UtcNow;
        public DateTime?        WithdrawalDate { get; set; }

        // ── Navegación ───────────────────────────────────────────────────────
        public Student.StudentEntity?   Student  { get; set; }
        public Grade.GradeEntity?       Grade    { get; set; }
        public Guardian.GuardianEntity? Guardian { get; set; }
    }
}
