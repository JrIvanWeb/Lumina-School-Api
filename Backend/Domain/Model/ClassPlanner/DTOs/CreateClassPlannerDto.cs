using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Domain.Model.ClassPlanner.DTOs
{
    public class CreateClassPlannerDto { 
        public Guid TeacherId { get; set; } 
        public Guid SubjectId { get; set; } 
        public Guid GradeId { get; set; } 
        public Guid AcademicPeriodId { get; set; } 
        public string Title { get; set; } = string.Empty; 
        public string Objectives { get; set; } = string.Empty; 
        public string Contents { get; set; } = string.Empty; 
        public string? Resources { get; set; } 
        public DateTime PlannedDate { get; set; } 
    }
}
