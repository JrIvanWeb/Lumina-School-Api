using LuminiSchool.Domain.Entities.Activity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Infrastructure.Repositories.Contract
{
    public interface IActivityRepository : IGenericRepository<ActivityEntity>
    {
        Task<IEnumerable<ActivityEntity>> GetByTeacherAsync(Guid teacherId);
        Task<IEnumerable<ActivityEntity>> GetByGradeAndSubjectAsync(Guid gradeId, Guid subjectId);
        Task<IEnumerable<ActivitySubmissionEntity>> GetSubmissionsAsync(Guid activityId);
        Task<ActivitySubmissionEntity?> GetSubmissionByStudentAsync(Guid activityId, Guid studentId);
        Task<ActivitySubmissionEntity> AddSubmissionAsync(ActivitySubmissionEntity submission);
        Task UpdateSubmissionAsync(ActivitySubmissionEntity submission);
    }
}
