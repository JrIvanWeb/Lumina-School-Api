using AutoMapper;
using LuminiSchool.Domain.Model.AcademicPeriod.DTOs;
using LuminiSchool.Domain.Entities.AcademicPeriod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Business.Utils.Profiles
{
    public class AcademicPeriodProfile: Profile
    {
        public AcademicPeriodProfile() {
            CreateMap<AcademicPeriodEntity, AcademicPeriodDto>();
            CreateMap<CreateAcademicPeriodDto, AcademicPeriodEntity>();
            CreateMap<UpdateAcademicPeriodDto, AcademicPeriodEntity>();
        }
    }
}
