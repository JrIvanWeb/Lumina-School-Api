using AutoMapper;
using LuminiSchool.Business.Services.Contract;
using LuminiSchool.Domain.Entities.Notification;
using LuminiSchool.Domain.Model.Notification.DTOs;
using LuminiSchool.Infrastructure.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Business.Services.Implementation
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _r; private readonly IMapper _m;
        public NotificationService(INotificationRepository r, IMapper m) { _r = r; _m = m; }
        public async Task<IEnumerable<NotificationDto>> GetByUserAsync(Guid uid) => _m.Map<IEnumerable<NotificationDto>>(await _r.GetByUserAsync(uid));
        public async Task<int> GetUnreadCountAsync(Guid uid) => await _r.GetUnreadCountAsync(uid);
        public async Task SendAsync(CreateNotificationDto dto) { var e = _m.Map<NotificationEntity>(dto); e.Id = Guid.NewGuid(); await _r.AddAsync(e); }
        public async Task MarkAsReadAsync(Guid id) => await _r.MarkAsReadAsync(id);
        public async Task MarkAllAsReadAsync(Guid uid) => await _r.MarkAllAsReadAsync(uid);
    }
}
