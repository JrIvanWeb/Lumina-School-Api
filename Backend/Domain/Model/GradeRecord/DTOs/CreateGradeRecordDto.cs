using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Domain.Model.GradeRecord.DTOs
{
    public class CreateGradeRecordDto { 
        public Guid StudentId { get; set; } 
        public Guid SubjectId { get; set; } 
        public Guid AcademicPeriodId { get; set; } 
        public decimal Score { get; set; } 
        public string? Observations { get; set; } 
    }
}
