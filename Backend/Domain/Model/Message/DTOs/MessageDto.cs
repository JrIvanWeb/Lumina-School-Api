namespace LuminiSchool.Domain.Model.Message.DTOs
{
    public class MessageDto { 
        public Guid Id{get;set;} 
        public Guid SenderId{get;set;} public Guid ReceiverId{get;set;} 
        public string Subject{get;set;}=string.Empty; 
        public string Body{get;set;}=string.Empty; 
        public bool IsRead{get;set;} public DateTime SentAt{get;set;} 
    }
}
