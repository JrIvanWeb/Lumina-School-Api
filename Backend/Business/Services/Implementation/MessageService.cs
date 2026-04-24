using AutoMapper;
using LuminiSchool.Business.Services.Contract;
using LuminiSchool.Domain.Entities.Message;
using LuminiSchool.Domain.Model.Message.DTOs;
using LuminiSchool.Infrastructure.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Business.Services.Implementation
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _r; private readonly IMapper _m;
        public MessageService(IMessageRepository r, IMapper m) { _r = r; _m = m; }
        public async Task<IEnumerable<MessageDto>> GetInboxAsync(Guid uid) => _m.Map<IEnumerable<MessageDto>>(await _r.GetInboxAsync(uid));
        public async Task<IEnumerable<MessageDto>> GetSentAsync(Guid uid) => _m.Map<IEnumerable<MessageDto>>(await _r.GetSentAsync(uid));
        public async Task SendAsync(CreateMessageDto dto, Guid senderId) { var e = _m.Map<MessageEntity>(dto); e.Id = Guid.NewGuid(); e.SenderId = senderId; e.SentAt = DateTime.UtcNow; await _r.AddAsync(e); }
    }
}
