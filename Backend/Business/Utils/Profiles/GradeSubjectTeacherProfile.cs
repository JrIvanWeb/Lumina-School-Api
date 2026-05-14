using AutoMapper;
using LuminiSchool.Domain.Entities.GradeSubjectTeacher;
using LuminiSchool.Domain.Model.GradeSubjectTeacher.DTOs;

namespace LuminiSchool.Business.Utils.Profiles
{
    public class GradeSubjectTeacherProfile : Profile
    {
        public GradeSubjectTeacherProfile()
        {
            CreateMap<GradeSubjectTeacherEntity, GradeSubjectTeacherDto>()
                .ForMember(d => d.GradeName,   o => o.MapFrom(s => s.Grade.Name))
                .ForMember(d => d.SubjectName, o => o.MapFrom(s => s.Subject.Name))
                .ForMember(d => d.TeacherName, o => o.MapFrom(s => s.Teacher.FirstName + " " + s.Teacher.LastName));

            CreateMap<CreateGradeSubjectTeacherDto, GradeSubjectTeacherEntity>();
        }
    }
}
