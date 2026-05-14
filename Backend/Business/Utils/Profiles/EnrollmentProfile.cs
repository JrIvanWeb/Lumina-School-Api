using AutoMapper;
using LuminiSchool.Domain.Entities.Enrollment;
using LuminiSchool.Domain.Entities.Parent;
using LuminiSchool.Domain.Model.Enrollment.DTOs;
using LuminiSchool.Domain.Model.Parent.DTOs;

namespace LuminiSchool.Business.Utils.Profiles
{
    public class EnrollmentProfile : Profile
    {
        public EnrollmentProfile()
        {
            CreateMap<EnrollmentEntity, EnrollmentDto>()
                .ForMember(d => d.StudentName,  o => o.MapFrom(s => s.Student  != null ? $"{s.Student.FirstName} {s.Student.LastName}" : ""))
                .ForMember(d => d.GradeName,    o => o.MapFrom(s => s.Grade    != null ? s.Grade.Name : ""))
                .ForMember(d => d.GuardianName, o => o.MapFrom(s => s.Guardian != null ? s.Guardian.FullName : ""))
                .ForMember(d => d.Student,      o => o.Ignore())
                .ForMember(d => d.Father,       o => o.Ignore())
                .ForMember(d => d.Mother,       o => o.Ignore())
                .ForMember(d => d.Guardian,     o => o.Ignore());

            CreateMap<CreateEnrollmentDto, EnrollmentEntity>();
            CreateMap<ParentEntity, ParentDto>();
        }
    }
}
