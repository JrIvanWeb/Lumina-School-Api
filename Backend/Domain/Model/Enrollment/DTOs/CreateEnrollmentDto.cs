using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Domain.Model.Enrollment.DTOs
{
    public class CreateEnrollmentDto { 
        public Guid StudentId { get; set; } 
        public Guid GradeId { get; set; } 
        public int AcademicYear { get; set; } 
    }
}
