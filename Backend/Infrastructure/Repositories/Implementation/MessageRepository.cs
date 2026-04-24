using LuminiSchool.Domain.Entities.Message;
using LuminiSchool.Infrastructure.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Infrastructure.Repositories.Implementation
{
    public class MessageRepository : GenericRepository<MessageEntity>, IMessageRepository
    {
        public MessageRepository(ApplicationDbContext ctx) : base(ctx) { }
        public async Task<IEnumerable<MessageEntity>> GetInboxAsync(Guid uid) => await _db.Where(m => m.ReceiverId == uid).OrderByDescending(m => m.SentAt).ToListAsync();
        public async Task<IEnumerable<MessageEntity>> GetSentAsync(Guid uid) => await _db.Where(m => m.SenderId == uid).OrderByDescending(m => m.SentAt).ToListAsync();
    }
}
