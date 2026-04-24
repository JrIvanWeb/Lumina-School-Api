using LuminiSchool.Domain.Entities.Report;

namespace LuminiSchool.Domain.Model.Report.DTOs
{
    public class ReportDto { 
        public Guid Id{get;set;} 
        public string Title{get;set;}=string.Empty; 
        public ReportType Type{get;set;} 
        public string? FileUrl{get;set;} 
        public DateTime GeneratedAt{get;set;} 
    }
    
}
