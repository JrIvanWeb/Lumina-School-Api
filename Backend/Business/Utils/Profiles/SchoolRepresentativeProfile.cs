using AutoMapper;
using LuminiSchool.Domain.Entities.SchoolRepresentative;
using LuminiSchool.Domain.Model.SchoolRepresentative.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Business.Utils.Profiles
{
    public class SchoolRepresentativeProfile: Profile
    {
        public SchoolRepresentativeProfile() {
            CreateMap<SchoolRepresentativeEntity, SchoolRepresentativeDto>()
                    .ForMember(d => d.StudentName, o => o.MapFrom(s => s.Student != null ? $"{s.Student.FirstName} {s.Student.LastName}" : ""));
            CreateMap<CreateSchoolRepresentativeDto, SchoolRepresentativeEntity>();
        }
    }
}
