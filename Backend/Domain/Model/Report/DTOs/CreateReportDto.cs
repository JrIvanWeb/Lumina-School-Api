using LuminiSchool.Domain.Entities.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Domain.Model.Report.DTOs
{
    public class CreateReportDto { 
        public string Title { get; set; } = string.Empty; 
        public ReportType Type { get; set; } 
        public string? Parameters { get; set; } 
    }
}
