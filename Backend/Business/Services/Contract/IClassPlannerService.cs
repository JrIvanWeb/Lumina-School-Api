using LuminiSchool.Domain.Model.ClassPlanner.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Business.Services.Contract
{
    public interface IClassPlannerService
    {
        Task<IEnumerable<ClassPlannerDto>> GetAllAsync();
        Task<ClassPlannerDto> GetByIdAsync(Guid id);
        Task<IEnumerable<ClassPlannerDto>> GetByTeacherAsync(Guid teacherId);
        Task<IEnumerable<ClassPlannerDto>> GetByGradeAndPeriodAsync(Guid gradeId, Guid periodId);
        Task<ClassPlannerDto> CreateAsync(CreateClassPlannerDto dto);
        Task<ClassPlannerDto> UpdateAsync(Guid id, UpdateClassPlannerDto dto);
        Task DeleteAsync(Guid id);
    }
}
