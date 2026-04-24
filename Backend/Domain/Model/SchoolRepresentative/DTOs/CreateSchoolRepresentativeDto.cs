using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Domain.Model.SchoolRepresentative.DTOs
{
    public class CreateSchoolRepresentativeDto { 
        public Guid StudentId { get; set; }
        public string Position { get; set; } = string.Empty; 
        public int AcademicYear { get; set; } 
    }
}
