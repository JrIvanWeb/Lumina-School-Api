using LuminiSchool.Domain.Entities.DiagnosticTest;

namespace LuminiSchool.Domain.Model.DiagnosticTest.DTOs
{
    public class DiagnosticTestDto { 
        public Guid Id{get;set;} 
        public Guid TeacherId{get;set;} 
        public Guid GradeId{get;set;} 
        public Guid SubjectId{get;set;} 
        public string Title{get;set;}=string.Empty; 
        public string Description{get;set;}=string.Empty; 
        public DiagnosticStatus Status{get;set;} public DateTime CreatedAt{get;set;} 
    }
}
