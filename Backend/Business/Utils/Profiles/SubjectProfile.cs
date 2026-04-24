using AutoMapper;
using LuminiSchool.Domain.Entities.Subject;
using LuminiSchool.Domain.Model.Subject.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Business.Utils.Profiles
{
    public class SubjectProfile: Profile
    {
        public SubjectProfile() {
            CreateMap<SubjectEntity, SubjectDto>();
            CreateMap<CreateSubjectDto, SubjectEntity>();
            CreateMap<UpdateSubjectDto, SubjectEntity>();
        }
    }
}
