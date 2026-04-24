using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Domain.Model.Teacher.DTOs
{
    public class CreateTeacherDto { 
        public string FirstName { get; set; } = string.Empty; 
        public string LastName { get; set; } = string.Empty; 
        public string DocumentNumber { get; set; } = string.Empty; 
        public string? Email { get; set; } 
        public string? Phone { get; set; } 
        public string? Specialty { get; set; } 
    }
}
