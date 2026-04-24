using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Domain.Entities.Bulletin
{
    public class BulletinEntity
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public Guid AcademicPeriodId { get; set; }
        public decimal GeneralAverage { get; set; }
        public string? GeneralObservations { get; set; }
        public string? PdfUrl { get; set; }
        public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;
        public Student.StudentEntity? Student { get; set; }
        public AcademicPeriod.AcademicPeriodEntity? AcademicPeriod { get; set; }
    }
}
