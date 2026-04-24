using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Domain.Model.AcademicPeriod.DTOs
{
    public class CreateAcademicPeriodDto { 
        public int AcademicYear { get; set; } 
        public int PeriodNumber { get; set; } 
        public DateTime StartDate { get; set; } 
        public DateTime EndDate { get; set; } 
    }
}
