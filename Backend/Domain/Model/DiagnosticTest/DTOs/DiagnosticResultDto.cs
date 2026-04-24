using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Domain.Model.DiagnosticTest.DTOs
{
    public class DiagnosticResultDto { 
        public Guid Id { get; set; } 
        public Guid StudentId { get; set; } 
        public string StudentName { get; set; } = string.Empty; 
        public decimal Score { get; set; } 
        public string? Observations { get; set; } 
        public DateTime TakenAt { get; set; } 
    }
}
