using LuminiSchool.Domain.Model.Notification.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Business.Services.Contract
{
    public interface INotificationService
    {
        Task<IEnumerable<NotificationDto>> GetByUserAsync(Guid userId);
        Task<int> GetUnreadCountAsync(Guid userId);
        Task SendAsync(CreateNotificationDto dto);
        Task MarkAsReadAsync(Guid id);
        Task MarkAllAsReadAsync(Guid userId);
    }
}
