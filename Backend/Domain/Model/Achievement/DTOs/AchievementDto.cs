using LuminiSchool.Domain.Entities.Achievement;

namespace LuminiSchool.Domain.Model.Achievement.DTOs
{
    public class AchievementDto { 
        public Guid Id{get;set;} 
        public Guid SubjectId{get;set;} 
        public string SubjectName{get;set;}=string.Empty; 
        public string Title{get;set;}=string.Empty; 
        public string Description{get;set;}=string.Empty; 
        public string Indicator{get;set;}=string.Empty; 
        public PerformanceLevel Level{get;set;} 
        public bool IsActive{get;set;} 
    }
    
}
