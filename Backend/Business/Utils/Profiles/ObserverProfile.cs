using AutoMapper;
using LuminiSchool.Domain.Entities.Observer;
using LuminiSchool.Domain.Model.Observer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Business.Utils.Profiles
{
    public class ObserverProfile: Profile
    {
        public ObserverProfile() {
            CreateMap<ObserverEntity, ObserverDto>()
                    .ForMember(d => d.StudentName, o => o.MapFrom(s => s.Student != null ? $"{s.Student.FirstName} {s.Student.LastName}" : ""))
                    .ForMember(d => d.TeacherName, o => o.MapFrom(s => s.Teacher != null ? $"{s.Teacher.FirstName} {s.Teacher.LastName}" : ""));
            CreateMap<CreateObserverDto, ObserverEntity>();
        }
    }
}
