namespace LuminiSchool.Domain.Model.Bulletin.DTOs
{
    public class BulletinDto { 
        public Guid Id{get;set;} 
        public Guid StudentId{get;set;} 
        public string StudentName{get;set;}=string.Empty; 
        public Guid AcademicPeriodId{get;set;} 
        public decimal GeneralAverage{get;set;} 
        public string? GeneralObservations{get;set;} 
        public string? PdfUrl{get;set;} public DateTime GeneratedAt{get;set;} 
    }
}
