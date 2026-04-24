using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Domain.Entities.IcfesSimulator
{
    public enum IcfesArea { Mathematics, Spanish, Sciences, SocialStudies, English, Philosophy }
    public enum IcfesSimulatorStatus { Scheduled, Active, Completed }

    public class IcfesSimulatorEntity
    {
        public Guid Id { get; set; }
        public Guid CoordinatorId { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime ScheduledAt { get; set; }
        public int DurationMinutes { get; set; }
        public int TotalQuestions { get; set; }
        public IcfesSimulatorStatus Status { get; set; } = IcfesSimulatorStatus.Scheduled;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<IcfesArea> Areas { get; set; } = new List<IcfesArea>();
        public ICollection<IcfesResultEntity> Results { get; set; } = new List<IcfesResultEntity>();
    }

    public class IcfesResultEntity
    {
        public Guid Id { get; set; }
        public Guid IcfesSimulatorId { get; set; }
        public Guid StudentId { get; set; }
        public decimal TotalScore { get; set; }
        public string? Feedback { get; set; }
        public string? CompetencyAnalysis { get; set; }
        public DateTime CompletedAt { get; set; } = DateTime.UtcNow;
        public IcfesSimulatorEntity? IcfesSimulator { get; set; }
        public Student.StudentEntity? Student { get; set; }
    }
}