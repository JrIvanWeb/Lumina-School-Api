using AutoMapper;
using LuminiSchool.Domain.Entities.Teacher;
using LuminiSchool.Domain.Model.Teacher.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Business.Utils.Profiles
{
    public class TeacherProfile: Profile
    {
        public TeacherProfile() {
            CreateMap<TeacherEntity, TeacherDto>();
            CreateMap<CreateTeacherDto, TeacherEntity>();
            CreateMap<UpdateTeacherDto, TeacherEntity>();
        }
    }
}
