using AutoMapper;
using LuminiSchool.Domain.Entities.Grade;
using LuminiSchool.Domain.Model.Grade.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Business.Utils.Profiles
{
    public class GradeProfile: Profile
    {
        public GradeProfile() {
            CreateMap<GradeEntity, GradeDto>();
            CreateMap<CreateGradeDto, GradeEntity>();
            CreateMap<UpdateGradeDto, GradeEntity>();
        }
    }
}
