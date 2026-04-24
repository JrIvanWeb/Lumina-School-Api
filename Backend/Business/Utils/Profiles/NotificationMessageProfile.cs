using AutoMapper;
using LuminiSchool.Domain.Entities.Message;
using LuminiSchool.Domain.Entities.Notification;
using LuminiSchool.Domain.Model.Message.DTOs;
using LuminiSchool.Domain.Model.Notification.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Business.Utils.Profiles
{
    public class NotificationMessageProfile: Profile
    {
        public NotificationMessageProfile() {
            CreateMap<NotificationEntity, NotificationDto>(); CreateMap<CreateNotificationDto, NotificationEntity>();
            CreateMap<MessageEntity, MessageDto>(); CreateMap<CreateMessageDto, MessageEntity>();
        }
    }
}
