using LuminiSchool.Domain.Model.Teacher.DTOs;

namespace LuminiSchool.Business.Services.Contract
{
    public interface ITeacherService
    {
        Task<IEnumerable<TeacherDto>> GetAllAsync();
        Task<TeacherDto>              GetByIdAsync(Guid id);
        Task<TeacherDto>              CreateAsync(CreateTeacherDto dto);
        Task<TeacherDto>              UpdateAsync(Guid id, UpdateTeacherDto dto);
        Task                          DeleteAsync(Guid id);
        Task                          ToggleActiveAsync(Guid id);
        Task                          AssignSubjectAsync(Guid teacherId, Guid subjectId);
        Task                          RemoveSubjectAsync(Guid teacherId, Guid subjectId);
    }
}
