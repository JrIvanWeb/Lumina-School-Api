using AutoMapper;
using LuminiSchool.Domain.Entities.Report;
using LuminiSchool.Domain.Model.Report.DTOs;

namespace LuminiSchool.Business.Utils.Profiles
{
    public class ReportProfile: Profile
    {
        public ReportProfile() {
            CreateMap<ReportEntity, ReportDto>(); 
            CreateMap<CreateReportDto, ReportEntity>();
        }
    }
}
