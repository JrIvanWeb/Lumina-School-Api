using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Domain.Model.IcfesSimulator.DTOs
{
    public class CreateIcfesResultDto { 
        public Guid IcfesSimulatorId { get; set; } 
        public Guid StudentId { get; set; } 
        public decimal TotalScore { get; set; } 
        public string? Feedback { get; set; } 
        public string? CompetencyAnalysis { get; set; } 
    }
}
