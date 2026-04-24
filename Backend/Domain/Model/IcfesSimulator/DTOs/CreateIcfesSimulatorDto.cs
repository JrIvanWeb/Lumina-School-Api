using LuminiSchool.Domain.Entities.IcfesSimulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Domain.Model.IcfesSimulator.DTOs
{
    public class CreateIcfesSimulatorDto { 
        public string Title { get; set; } = string.Empty; 
        public DateTime ScheduledAt { get; set; } 
        public int DurationMinutes { get; set; } 
        public int TotalQuestions { get; set; } 
        public List<IcfesArea> Areas { get; set; } = new(); 
    }
}
