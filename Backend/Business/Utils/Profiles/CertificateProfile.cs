using AutoMapper;
using LuminiSchool.Domain.Entities.Certificate;
using LuminiSchool.Domain.Model.Certificate.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Business.Utils.Profiles
{
    public class CertificateProfile: Profile
    {
          public CertificateProfile() {
            CreateMap<CertificateEntity, CertificateDto>()
                    .ForMember(d => d.StudentName, o => o.MapFrom(s => s.Student != null ? $"{s.Student.FirstName} {s.Student.LastName}" : ""));

        }
    }
}
