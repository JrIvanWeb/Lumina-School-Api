using AutoMapper;
using LuminiSchool.Domain.Entities.DiagnosticTest;
using LuminiSchool.Domain.Model.DiagnosticTest.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Business.Utils.Profiles
{
    public class DiagnosticTestProfile: Profile
    {
        public DiagnosticTestProfile() {
            CreateMap<DiagnosticTestEntity, DiagnosticTestDto>(); CreateMap<CreateDiagnosticTestDto, DiagnosticTestEntity>();
            CreateMap<DiagnosticResultEntity, DiagnosticResultDto>()
                .ForMember(d => d.StudentName, o => o.MapFrom(s => s.Student != null ? $"{s.Student.FirstName} {s.Student.LastName}" : ""));
            CreateMap<CreateDiagnosticResultDto, DiagnosticResultEntity>();
        }
    }
}
