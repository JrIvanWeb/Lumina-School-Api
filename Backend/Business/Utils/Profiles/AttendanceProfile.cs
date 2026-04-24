using AutoMapper;
using LuminiSchool.Domain.Entities.Attendance;
using LuminiSchool.Domain.Model.Attendance.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Business.Utils.Profiles
{
    public class AttendanceProfile: Profile
    {
        public AttendanceProfile() {
            CreateMap<AttendanceEntity, AttendanceDto>()
                    .ForMember(d => d.StudentName, o => o.MapFrom(s => s.Student != null ? $"{s.Student.FirstName} {s.Student.LastName}" : ""));
            CreateMap<CreateAttendanceDto, AttendanceEntity>();
        }
    }
}
