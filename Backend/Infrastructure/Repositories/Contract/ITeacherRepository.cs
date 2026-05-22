using LuminiSchool.Domain.Entities.Teacher;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LuminiSchool.Infrastructure.Repositories.Contract
{
    public interface ITeacherRepository : IGenericRepository<TeacherEntity>
    {
        Task<TeacherEntity?> GetByDocumentAsync(string doc);
        Task<IEnumerable<TeacherEntity>> GetWithSubjectsAsync();
        Task AssignSubjectAsync(Guid teacherId, Guid subjectId);
        Task RemoveSubjectAsync(Guid teacherId, Guid subjectId);
        Task<TeacherEntity?> GetByUserIdAsync(Guid userId);   // ← NUEVO
    }
}
