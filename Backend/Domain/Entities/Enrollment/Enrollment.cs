using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Domain.Entities.Enrollment
{
    public enum EnrollmentStatus { Active, Withdrawn, Graduated, Transferred }

    public class EnrollmentEntity
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public Guid GradeId { get; set; }
        public int AcademicYear { get; set; }
        public EnrollmentStatus Status { get; set; } = EnrollmentStatus.Active;
        public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;
        public DateTime? WithdrawalDate { get; set; }
        public Student.StudentEntity? Student { get; set; }
        public Grade.GradeEntity? Grade { get; set; }
    }
}