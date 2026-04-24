namespace LuminiSchool.Domain.Model.Grade.DTOs
{
    public class GradeDto { 
        public Guid Id{get;set;} 
        public string Name{get;set;}=string.Empty; 
        public string Level{get;set;}=string.Empty; 
        public string? Section{get;set;} 
        public bool IsActive{get;set;} 
    }
}
