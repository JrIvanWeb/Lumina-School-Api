using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Domain.Model.DiagnosticTest.DTOs
{
    public class CreateDiagnosticTestDto { 
        public Guid TeacherId { get; set; } 
        public Guid GradeId { get; set; } 
        public Guid SubjectId { get; set; } 
        public string Title { get; set; } = string.Empty; 
        public string Description { get; set; } = string.Empty; 
    }
}
