using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Domain.Model.Guardian.DTOs
{
    public class CreateGuardianDto { 
        public string FirstName { get; set; } = string.Empty; 
        public string LastName { get; set; } = string.Empty; 
        public string DocumentNumber { get; set; } = string.Empty; 
        public string Relationship { get; set; } = string.Empty; 
        public string? Email { get; set; } 
        public string Phone { get; set; } = string.Empty; 
        public string? Address { get; set; } public List<Guid> StudentIds { get; set; } = new(); 
    }
}
