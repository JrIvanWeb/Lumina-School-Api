using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Domain.Entities.AcademicPeriod
{
    public class AcademicPeriodEntity
    {
        public Guid Id { get; set; }
        public int AcademicYear { get; set; }
        public int PeriodNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? Name { get; set; }
    }
}