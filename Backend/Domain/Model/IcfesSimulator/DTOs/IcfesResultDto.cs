using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Domain.Model.IcfesSimulator.DTOs
{
    public class IcfesResultDto { 
        public Guid Id { get; set; } 
        public Guid StudentId { get; set; } 
        public string StudentName { get; set; } = string.Empty; 
        public decimal TotalScore { get; set; } 
        public string? Feedback { get; set; } 
        public string? CompetencyAnalysis { get; set; } 
        public DateTime CompletedAt { get; set; } 
    }
}
