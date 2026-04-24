namespace LuminiSchool.Domain.Model.ClassPlanner.DTOs
{
    public class ClassPlannerDto { 
        public Guid Id{get;set;} 
        public Guid TeacherId{get;set;} 
        public string TeacherName{get;set;}=string.Empty; 
        public Guid SubjectId{get;set;} 
        public string SubjectName{get;set;}=string.Empty; 
        public Guid GradeId{get;set;} 
        public string GradeName{get;set;}=string.Empty; 
        public Guid AcademicPeriodId{get;set;} 
        public string Title{get;set;}=string.Empty; 
        public string Objectives{get;set;}=string.Empty; 
        public string Contents{get;set;}=string.Empty; 
        public string? Resources{get;set;} 
        public DateTime PlannedDate{get;set;} 
    }
}
