using AutoMapper;
using LuminiSchool.Domain.Entities.Bulletin;
using LuminiSchool.Domain.Model.Bulletin.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Business.Utils.Profiles
{
    public class BulletinProfile: Profile
    {
        public BulletinProfile() {
            CreateMap<BulletinEntity, BulletinDto>()
                .ForMember(d => d.StudentName, o => o.MapFrom(s => s.Student != null ? $"{s.Student.FirstName} {s.Student.LastName}" : ""));
        }
    }
}
