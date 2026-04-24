using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Domain.Model.Activity.DTOs
{
    public class GradeSubmissionDto { 
        public Guid SubmissionId { get; set; } 
        public decimal Score { get; set; } 
        public string? Feedback { get; set; } 
    }
}
