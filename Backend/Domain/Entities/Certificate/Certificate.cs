using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Domain.Entities.Certificate
{
    public enum CertificateType { Enrollment, Grades, Attendance, Diploma, General }

    public class CertificateEntity
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public CertificateType Type { get; set; }
        public string? PdfUrl { get; set; }
        public DateTime IssuedAt { get; set; } = DateTime.UtcNow;
        public Guid IssuedByUserId { get; set; }
        public Student.StudentEntity? Student { get; set; }
    }
}