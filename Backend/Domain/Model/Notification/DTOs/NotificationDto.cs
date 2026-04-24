using LuminiSchool.Domain.Entities.Notification;

namespace LuminiSchool.Domain.Model.Notification.DTOs
{
    public class NotificationDto { public Guid Id{get;set;} public string Title{get;set;}=string.Empty; public string Body{get;set;}=string.Empty; public NotificationType Type{get;set;} public bool IsRead{get;set;} public DateTime CreatedAt{get;set;} }
}
