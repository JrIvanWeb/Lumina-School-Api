using AutoMapper;
using LuminiSchool.Domain.Entities.GradeRecord;
using LuminiSchool.Domain.Model.GradeRecord.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Business.Utils.Profiles
{
    public class GradeRecordProfile: Profile
    {
        public GradeRecordProfile() {
            CreateMap<GradeRecordEntity, GradeRecordDto>()
                  .ForMember(d => d.StudentName, o => o.MapFrom(s => s.Student != null ? $"{s.Student.FirstName} {s.Student.LastName}" : ""))
                  .ForMember(d => d.SubjectName, o => o.MapFrom(s => s.Subject != null ? s.Subject.Name : ""));
            CreateMap<CreateGradeRecordDto, GradeRecordEntity>();
            CreateMap<UpdateGradeRecordDto, GradeRecordEntity>();
        }
    }
}
