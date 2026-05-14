using AutoMapper;
using LuminiSchool.Domain.Entities.Teacher;
using LuminiSchool.Domain.Model.Teacher.DTOs;

namespace LuminiSchool.Business.Utils.Profiles
{
    public class TeacherProfile : Profile
    {
        public TeacherProfile()
        {
            // Entity → DTO
            CreateMap<TeacherEntity, TeacherDto>()
                .ForMember(d => d.FullName,
                    o => o.MapFrom(s => $"{s.FirstName} {s.LastName}".Trim()))
                .ForMember(d => d.BirthDate,
                    o => o.MapFrom(s => s.BirthDate.HasValue
                        ? s.BirthDate.Value.ToString("yyyy-MM-dd") : null))
                .ForMember(d => d.HireDate,
                    o => o.MapFrom(s => s.HireDate.HasValue
                        ? s.HireDate.Value.ToString("yyyy-MM-dd") : null));

            // CreateDto → Entity
            CreateMap<CreateTeacherDto, TeacherEntity>()
                .ForMember(d => d.BirthDate,
                    o => o.MapFrom(s => string.IsNullOrWhiteSpace(s.BirthDate)
                        ? (DateTime?)null : DateTime.Parse(s.BirthDate)))
                .ForMember(d => d.HireDate,
                    o => o.MapFrom(s => string.IsNullOrWhiteSpace(s.HireDate)
                        ? (DateTime?)null : DateTime.Parse(s.HireDate)))
                .ForMember(d => d.Id,        o => o.Ignore())
                .ForMember(d => d.UserId,    o => o.Ignore())
                .ForMember(d => d.IsActive,  o => o.Ignore())
                .ForMember(d => d.CreatedAt, o => o.Ignore())
                .ForMember(d => d.UpdatedAt, o => o.Ignore())
                .ForMember(d => d.Subjects,  o => o.Ignore());

            // UpdateDto → Entity (solo sobreescribe campos no-nulos)
            CreateMap<UpdateTeacherDto, TeacherEntity>()
                .ForAllMembers(o => o.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
