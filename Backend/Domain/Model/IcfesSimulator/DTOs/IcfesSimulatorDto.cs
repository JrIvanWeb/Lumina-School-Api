using LuminiSchool.Domain.Entities.IcfesSimulator;

namespace LuminiSchool.Domain.Model.IcfesSimulator.DTOs
{
    public class IcfesSimulatorDto { 
        public Guid Id{get;set;} 
        public string Title{get;set;}=string.Empty; 
        public DateTime ScheduledAt{get;set;} 
        public int DurationMinutes{get;set;} 
        public int TotalQuestions{get;set;} 
        public IcfesSimulatorStatus Status{get;set;} 
        public List<IcfesArea> Areas{get;set;}=new(); 
    }
}
