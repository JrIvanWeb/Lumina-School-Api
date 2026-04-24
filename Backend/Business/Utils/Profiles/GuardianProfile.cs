using AutoMapper;
using LuminiSchool.Domain.Entities.Guardian;
using LuminiSchool.Domain.Model.Guardian.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Business.Utils.Profiles
{
    public class GuardianProfile: Profile
    {
        public GuardianProfile() {
            CreateMap<GuardianEntity, GuardianDto>();
            CreateMap<CreateGuardianDto, GuardianEntity>();
            CreateMap<UpdateGuardianDto, GuardianEntity>();
        }
    }
}
