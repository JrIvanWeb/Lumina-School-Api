using LuminiSchool.Domain.Entities.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Infrastructure.Repositories.Contract
{
    public interface INotificationRepository : IGenericRepository<NotificationEntity>
    {
        Task<IEnumerable<NotificationEntity>> GetByUserAsync(Guid userId);
        Task<int> GetUnreadCountAsync(Guid userId);
        Task MarkAsReadAsync(Guid id);
        Task MarkAllAsReadAsync(Guid userId);
    }
}
