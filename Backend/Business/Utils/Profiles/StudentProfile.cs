using AutoMapper;
using LuminiSchool.Domain.Entities.Student;
using LuminiSchool.Domain.Model.Student.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Business.Utils.Profiles
{
    public class StudentProfile: Profile
    {
        public StudentProfile() {
            CreateMap<StudentEntity, StudentDto>();
            CreateMap<CreateStudentDto, StudentEntity>();
            CreateMap<UpdateStudentDto, StudentEntity>();
        }
    }
}
