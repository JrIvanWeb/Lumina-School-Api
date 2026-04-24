using LuminiSchool.Domain.Model.Schedule.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Business.Services.Contract
{
    public interface IScheduleService
    {
        Task<IEnumerable<ScheduleDto>> GetByGradeAsync(Guid gradeId);
        Task<IEnumerable<ScheduleDto>> GetByTeacherAsync(Guid teacherId);
        Task<ScheduleDto> CreateAsync(CreateScheduleDto dto);
        Task DeleteAsync(Guid id);
    }
}
