using AutoMapper;
using LuminiSchool.Domain.Entities.Activity;
using LuminiSchool.Domain.Model.Activity.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Business.Utils.Profiles
{
    public class ActivityProfile: Profile
    {
        public ActivityProfile() {
            CreateMap<ActivityEntity, ActivityDto>()
                   .ForMember(d => d.SubjectName, o => o.MapFrom(s => s.Subject != null ? s.Subject.Name : ""));
            CreateMap<CreateActivityDto, ActivityEntity>();
            CreateMap<ActivitySubmissionEntity, ActivitySubmissionDto>()
                .ForMember(d => d.StudentName, o => o.MapFrom(s => s.Student != null ? $"{s.Student.FirstName} {s.Student.LastName}" : ""));
        }
    }
}
