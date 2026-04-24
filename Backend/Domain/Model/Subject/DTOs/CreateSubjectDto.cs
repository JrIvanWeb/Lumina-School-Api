using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Domain.Model.Subject.DTOs
{
    public class CreateSubjectDto { 
        public string Name { get; set; } = string.Empty; 
        public string? Description { get; set; } 
        public int WeeklyHours { get; set; } 
    }
}
