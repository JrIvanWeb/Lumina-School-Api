using LuminiSchool.Domain.Entities.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Domain.Model.Notification.DTOs
{
    public class CreateNotificationDto { 
        public Guid UserId { get; set; } 
        public string Title { get; set; } = string.Empty; 
        public string Body { get; set; } = string.Empty; 
        public NotificationType Type { get; set; } 
    }
}
