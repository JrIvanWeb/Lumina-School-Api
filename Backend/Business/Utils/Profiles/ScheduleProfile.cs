using AutoMapper;
using LuminiSchool.Domain.Entities.Schedule;
using LuminiSchool.Domain.Model.Schedule.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Business.Utils.Profiles
{
    public class ScheduleProfile: Profile
    {
        public ScheduleProfile() {
            CreateMap<ScheduleEntity, ScheduleDto>()
                .ForMember(d => d.GradeName, o => o.MapFrom(s => s.Grade != null ? s.Grade.Name : ""))
                .ForMember(d => d.SubjectName, o => o.MapFrom(s => s.Subject != null ? s.Subject.Name : ""))
                .ForMember(d => d.TeacherName, o => o.MapFrom(s => s.Teacher != null ? $"{s.Teacher.FirstName} {s.Teacher.LastName}" : ""));
            CreateMap<CreateScheduleDto, ScheduleEntity>();
        }
    }
}
