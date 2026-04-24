using AutoMapper;
using LuminiSchool.Domain.Entities.Achievement;
using LuminiSchool.Domain.Model.Achievement.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Business.Utils.Profiles
{
    public class AchievementProfile: Profile
    {
        public AchievementProfile() {
            CreateMap<AchievementEntity, AchievementDto>()
                    .ForMember(d => d.SubjectName, o => o.MapFrom(s => s.Subject != null ? s.Subject.Name : ""));
            CreateMap<CreateAchievementDto, AchievementEntity>();
        }
    }
}
