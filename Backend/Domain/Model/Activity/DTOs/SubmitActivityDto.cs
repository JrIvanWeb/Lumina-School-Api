using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Domain.Model.Activity.DTOs
{
    public class SubmitActivityDto { 
        public Guid ActivityId { get; set; } 
        public Guid StudentId { get; set; } 
        public string? Comments { get; set; } 
        public string? FileUrl { get; set; } 
    }
}
