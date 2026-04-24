using LuminiSchool.Domain.Entities.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Infrastructure.Repositories.Contract
{
    public interface IMessageRepository : IGenericRepository<MessageEntity>
    {
        Task<IEnumerable<MessageEntity>> GetInboxAsync(Guid userId);
        Task<IEnumerable<MessageEntity>> GetSentAsync(Guid userId);
    }
}
