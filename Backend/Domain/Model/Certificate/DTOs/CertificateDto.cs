using LuminiSchool.Domain.Entities.Certificate;

namespace LuminiSchool.Domain.Model.Certificate.DTOs
{
    public class CertificateDto { 
        public Guid Id{get;set;} 
        public Guid StudentId{get;set;} 
        public string StudentName{get;set;}=string.Empty; 
        public CertificateType Type{get;set;} 
        public string? PdfUrl{get;set;} 
        public DateTime IssuedAt{get;set;} 
    }
}
