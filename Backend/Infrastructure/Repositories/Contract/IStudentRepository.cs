using LuminiSchool.Domain.Entities.Student;

namespace LuminiSchool.Infrastructure.Repositories.Contract
{
    public interface IStudentRepository : IGenericRepository<StudentEntity>
    {
        Task<StudentEntity?> GetByDocumentAsync(string documentNumber);
        Task<IEnumerable<StudentEntity>> GetByGradeAsync(Guid gradeId);
        Task<IEnumerable<StudentEntity>> GetBirthdaysTodayAsync();
    }
}