using LuminiSchool.Domain.Entities.Activity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Domain.Model.Activity.DTOs
{
    public class CreateActivityDto { 
        public Guid TeacherId { get; set; } 
        public Guid SubjectId { get; set; } 
        public Guid GradeId { get; set; } 
        public Guid AcademicPeriodId { get; set; } 
        public string Title { get; set; } = string.Empty; 
        public string Description { get; set; } = string.Empty; 
        public ActivityType Type { get; set; } 
        public decimal MaxScore { get; set; } 
        public DateTime DueDate { get; set; } 
    }
}
