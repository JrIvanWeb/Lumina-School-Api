using AutoMapper;
using LuminiSchool.Domain.Entities.Subject;
using LuminiSchool.Domain.Entities.Grade;
using LuminiSchool.Domain.Model.Subject.DTOs;

namespace LuminiSchool.Business.Utils.Profiles
{
    public class SubjectProfile : Profile
    {
        public SubjectProfile()
        {
            // GradeEntity → SubjectGradeDto (para la lista de grados dentro de SubjectDto)
            CreateMap<GradeEntity, SubjectGradeDto>();

            // SubjectEntity → SubjectDto (incluye la lista de grados mapeada arriba)
            CreateMap<SubjectEntity, SubjectDto>();

            CreateMap<CreateSubjectDto, SubjectEntity>();
            CreateMap<UpdateSubjectDto, SubjectEntity>();
        }
    }
}

