using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Domain.Model.Grade.DTOs
{
    public class CreateGradeDto { 
        public string Name { get; set; } = string.Empty; 
        public string Level { get; set; } = string.Empty;
        public string? Section { get; set; } 
    }
}
