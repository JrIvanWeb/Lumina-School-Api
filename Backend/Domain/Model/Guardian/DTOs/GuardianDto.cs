namespace LuminiSchool.Domain.Model.Guardian.DTOs
{
    public class GuardianDto { 
        public Guid Id{get;set;} 
        public string FirstName{get;set;}=string.Empty; 
        public string LastName{get;set;}=string.Empty; 
        public string FullName=>$"{FirstName} {LastName}"; 
        public string DocumentNumber{get;set;}=string.Empty; 
        public string Relationship{get;set;}=string.Empty; 
        public string? Email{get;set;} public string Phone{get;set;}=string.Empty; 
        public string? Address{get;set;} public bool IsActive{get;set;} 
    }
}
