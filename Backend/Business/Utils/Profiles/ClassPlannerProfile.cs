using AutoMapper;
using LuminiSchool.Domain.Entities.ClassPlanner;
using LuminiSchool.Domain.Model.ClassPlanner.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Business.Utils.Profiles
{
    public class ClassPlannerProfile: Profile
    {
        public ClassPlannerProfile() { 
         CreateMap<ClassPlannerEntity, ClassPlannerDto>()
            .ForMember(d => d.TeacherName, o => o.MapFrom(s => s.Teacher != null ? $"{s.Teacher.FirstName} {s.Teacher.LastName}" : ""))
                .ForMember(d => d.SubjectName, o => o.MapFrom(s => s.Subject != null ? s.Subject.Name : ""))
                .ForMember(d => d.GradeName, o => o.MapFrom(s => s.Grade != null ? s.Grade.Name : ""));
            CreateMap<CreateClassPlannerDto, ClassPlannerEntity>();
         CreateMap<UpdateClassPlannerDto, ClassPlannerEntity>();

        }
    }
}
