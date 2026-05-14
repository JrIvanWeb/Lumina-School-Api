using LuminiSchool.Domain.Entities.GradeSubjectTeacher;

namespace LuminiSchool.Infrastructure.Repositories.Contract
{
    public interface IGradeSubjectTeacherRepository
    {
        Task<IEnumerable<GradeSubjectTeacherEntity>> GetAllAsync();
        Task<IEnumerable<GradeSubjectTeacherEntity>> GetByGradeAsync(Guid gradeId);
        Task<GradeSubjectTeacherEntity?> GetByIdAsync(Guid id);
        Task<bool> ExistsAsync(Guid gradeId, Guid subjectId, Guid teacherId);
        Task<GradeSubjectTeacherEntity> AddAsync(GradeSubjectTeacherEntity entity);
        Task UpdateAsync(GradeSubjectTeacherEntity entity);
        Task DeleteAsync(Guid id);
    }
}
