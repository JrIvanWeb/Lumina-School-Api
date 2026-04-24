using LuminiSchool.Domain.Entities.Notification;
using LuminiSchool.Infrastructure.Repositories.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Infrastructure.Repositories.Implementation
{
    public class NotificationRepository : GenericRepository<NotificationEntity>, INotificationRepository
    {
        public NotificationRepository(ApplicationDbContext ctx) : base(ctx) { }
        public async Task<IEnumerable<NotificationEntity>> GetByUserAsync(Guid uid) => await _db.Where(n => n.UserId == uid).OrderByDescending(n => n.CreatedAt).ToListAsync();
        public async Task<int> GetUnreadCountAsync(Guid uid) => await _db.CountAsync(n => n.UserId == uid && !n.IsRead);
        public async Task MarkAsReadAsync(Guid id) { var n = await _db.FindAsync(id); if (n != null) { n.IsRead = true; await _ctx.SaveChangesAsync(); } }
        public async Task MarkAllAsReadAsync(Guid uid) { var list = await _db.Where(n => n.UserId == uid && !n.IsRead).ToListAsync(); list.ForEach(n => n.IsRead = true); await _ctx.SaveChangesAsync(); }
    }
}
