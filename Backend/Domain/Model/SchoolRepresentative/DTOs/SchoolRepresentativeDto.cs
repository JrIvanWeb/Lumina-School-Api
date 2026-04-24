namespace LuminiSchool.Domain.Model.SchoolRepresentative.DTOs
{
    public class SchoolRepresentativeDto { 
        public Guid Id{get;set;} public Guid StudentId{get;set;} 
        public string StudentName{get;set;}=string.Empty; 
        public string Position{get;set;}=string.Empty; 
        public int AcademicYear{get;set;} 
        public bool IsActive{get;set;} 
    }
    
}
