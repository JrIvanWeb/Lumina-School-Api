using AutoMapper;
using LuminiSchool.Domain.Entities.Enrollment;
using LuminiSchool.Domain.Model.Enrollment.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Business.Utils.Profiles
{
    public class EnrollmentProfile: Profile
    {
        public EnrollmentProfile() { 
          CreateMap<EnrollmentEntity, EnrollmentDto>()
            .ForMember(d => d.StudentName, o => o.MapFrom(s => s.Student != null ? $"{s.Student.FirstName} {s.Student.LastName}" : ""))
                .ForMember(d => d.GradeName, o => o.MapFrom(s => s.Grade != null ? s.Grade.Name : ""));

            CreateMap<CreateEnrollmentDto, EnrollmentEntity>();
        }
    }
}
