using LuminiSchool.Domain.Entities.Achievement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Domain.Model.Achievement.DTOs
{
    public class CreateAchievementDto
    {
        public Guid SubjectId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Indicator { get; set; } = string.Empty;
        public PerformanceLevel Level { get; set; }
    }
}
