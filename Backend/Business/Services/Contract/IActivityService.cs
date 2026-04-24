using LuminiSchool.Domain.Model.Activity.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Business.Services.Contract
{
    public interface IActivityService
    {
        Task<IEnumerable<ActivityDto>> GetAllAsync();
        Task<ActivityDto> GetByIdAsync(Guid id);
        Task<IEnumerable<ActivityDto>> GetByTeacherAsync(Guid teacherId);
        Task<ActivityDto> CreateAsync(CreateActivityDto dto);
        Task DeleteAsync(Guid id);
        Task<ActivitySubmissionDto> SubmitAsync(SubmitActivityDto dto);
        Task<ActivitySubmissionDto> GradeSubmissionAsync(GradeSubmissionDto dto);
        Task<IEnumerable<ActivitySubmissionDto>> GetSubmissionsAsync(Guid activityId);
    }
}
