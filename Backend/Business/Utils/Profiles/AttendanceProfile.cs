using AutoMapper;
using LuminiSchool.Domain.Entities.Attendance;
using LuminiSchool.Domain.Model.Attendance.DTOs;

namespace LuminiSchool.Business.Utils.Profiles
{
    public class AttendanceProfile : Profile
    {
        public AttendanceProfile()
        {
            CreateMap<AttendanceEntity, AttendanceDto>()
                .ForMember(d => d.StudentName, o => o.MapFrom(s =>
                    s.Student != null ? $"{s.Student.FirstName} {s.Student.LastName}" : ""))
                .ForMember(d => d.SubjectName, o => o.MapFrom(s =>
                    s.Subject != null ? s.Subject.Name : null));

            CreateMap<CreateAttendanceDto, AttendanceEntity>();
        }
    }
}
