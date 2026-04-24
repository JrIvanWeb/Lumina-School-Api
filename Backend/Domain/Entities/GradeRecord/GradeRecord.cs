using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Domain.Entities.GradeRecord
{
    public class GradeRecordEntity
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public Guid SubjectId { get; set; }
        public Guid AcademicPeriodId { get; set; }
        public decimal Score { get; set; }
        public decimal? Average { get; set; }
        public string? Observations { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public Student.StudentEntity? Student { get; set; }
        public Subject.SubjectEntity? Subject { get; set; }
        public AcademicPeriod.AcademicPeriodEntity? AcademicPeriod { get; set; }
    }
}
