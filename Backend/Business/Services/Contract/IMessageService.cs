using LuminiSchool.Domain.Model.Message.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Business.Services.Contract
{
    public interface IMessageService
    {
        Task<IEnumerable<MessageDto>> GetInboxAsync(Guid userId);
        Task<IEnumerable<MessageDto>> GetSentAsync(Guid userId);
        Task SendAsync(CreateMessageDto dto, Guid senderId);
    }
}
