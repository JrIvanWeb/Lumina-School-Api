using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Domain.Model.Message.DTOs
{
    public class CreateMessageDto { 
        public Guid ReceiverId { get; set; } 
        public string Subject { get; set; } = string.Empty; 
        public string Body { get; set; } = string.Empty; 
    }
}
