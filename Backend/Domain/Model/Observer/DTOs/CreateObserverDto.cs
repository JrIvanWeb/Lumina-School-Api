using LuminiSchool.Domain.Entities.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Domain.Model.Observer.DTOs
{
    public class CreateObserverDto { 
        public Guid StudentId { get; set; } public Guid TeacherId { get; set; } 
        public ObserverType Type { get; set; } 
        public string Description { get; set; } = string.Empty; 
    }
}
