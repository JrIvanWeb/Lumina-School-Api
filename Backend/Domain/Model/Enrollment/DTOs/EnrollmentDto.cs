using LuminiSchool.Domain.Entities.Enrollment;

namespace LuminiSchool.Domain.Model.Enrollment.DTOs
{
    public class EnrollmentDto { 
        public Guid Id{get;set;} 
        public Guid StudentId{get;set;} 
        public string StudentName{get;set;}=string.Empty; 
        public Guid GradeId{get;set;} 
        public string GradeName{get;set;}=string.Empty; 
        public int AcademicYear{get;set;} 
        public EnrollmentStatus Status{get;set;} 
        public DateTime EnrollmentDate{get;set;} 
    }
}
