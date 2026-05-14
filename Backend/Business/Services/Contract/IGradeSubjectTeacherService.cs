using LuminiSchool.Domain.Model.GradeSubjectTeacher.DTOs;

namespace LuminiSchool.Business.Services.Contract
{
    public interface IGradeSubjectTeacherService
    {
        Task<IEnumerable<GradeSubjectTeacherDto>> GetAllAsync();
        Task<IEnumerable<GradeSubjectTeacherDto>> GetByGradeAsync(Guid gradeId);
        Task<GradeSubjectTeacherDto> GetByIdAsync(Guid id);
        Task<GradeSubjectTeacherDto> CreateAsync(CreateGradeSubjectTeacherDto dto);
        Task<GradeSubjectTeacherDto> UpdateAsync(Guid id, UpdateGradeSubjectTeacherDto dto);
        Task DeleteAsync(Guid id);
    }
}
