using LuminiSchool.Domain.Entities.Guardian;

namespace LuminiSchool.Infrastructure.Repositories.Contract
{
    public interface IGuardianRepository : IGenericRepository<GuardianEntity>
    {
        Task<IEnumerable<GuardianEntity>> GetByStudentAsync(Guid studentId);
        Task<GuardianEntity?>             GetByDocumentAsync(string documentNumber);
        Task                              AssignStudentAsync(Guid guardianId, Guid studentId);
        Task                              AddOrUpdateAsync(GuardianEntity guardian);
    }
}
