using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Domain.Model.DiagnosticTest.DTOs
{
    public class CreateDiagnosticResultDto { 
        public Guid DiagnosticTestId { get; set; } 
        public Guid StudentId { get; set; } 
        public decimal Score { get; set; } 
        public string? Observations { get; set; } 
    }
}
