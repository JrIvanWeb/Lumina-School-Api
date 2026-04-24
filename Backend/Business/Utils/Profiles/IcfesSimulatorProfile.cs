using AutoMapper;
using LuminiSchool.Domain.Entities.IcfesSimulator;
using LuminiSchool.Domain.Model.IcfesSimulator.DTOs;

namespace LuminiSchool.Business.Utils.Profiles
{
    public class IcfesSimulatorProfile : Profile
    {
        public IcfesSimulatorProfile()
        {
            CreateMap<IcfesSimulatorEntity, IcfesSimulatorDto>(); CreateMap<CreateIcfesSimulatorDto, IcfesSimulatorEntity>();
            CreateMap<IcfesResultEntity, IcfesResultDto>()
                .ForMember(d => d.StudentName, o => o.MapFrom(s => s.Student != null ? $"{s.Student.FirstName} {s.Student.LastName}" : ""));
            CreateMap<CreateIcfesResultDto, IcfesResultEntity>();
        }
    }
}
