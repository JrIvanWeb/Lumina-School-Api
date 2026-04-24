using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Domain.Entities.Achievement
{
    public enum PerformanceLevel { Superior, High, Basic, Low }

    public class AchievementEntity
    {
        public Guid Id { get; set; }
        public Guid SubjectId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Indicator { get; set; } = string.Empty;
        public PerformanceLevel Level { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Subject.SubjectEntity? Subject { get; set; }
    }
}
