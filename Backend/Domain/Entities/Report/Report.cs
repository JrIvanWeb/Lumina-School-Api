using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Domain.Entities.Report
{
    public enum ReportType { Grades, Attendance, Performance, General, Statistics }

    public class ReportEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public ReportType Type { get; set; }
        public string? Parameters { get; set; }
        public string? FileUrl { get; set; }
        public Guid GeneratedByUserId { get; set; }
        public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;
    }
}
