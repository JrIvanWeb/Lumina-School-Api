using LuminiSchool.Domain.Entities.Certificate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Domain.Model.Certificate.DTOs
{
    public class CreateCertificateDto { 
        public Guid StudentId { get; set; } 
        public CertificateType Type { get; set; } 
    }
}
