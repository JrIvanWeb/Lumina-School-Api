using LuminiSchool.Domain.Entities.Observer;

namespace LuminiSchool.Domain.Model.Observer.DTOs
{
    public class ObserverDto { 
        public Guid Id{get;set;} 
        public Guid StudentId{get;set;} 
        public string StudentName{get;set;}=string.Empty; 
        public Guid TeacherId{get;set;} 
        public string TeacherName{get;set;}=string.Empty; 
        public ObserverType Type{get;set;} 
        public string Description{get;set;}=string.Empty; 
        public DateTime Date{get;set;} 
    }
    
}
