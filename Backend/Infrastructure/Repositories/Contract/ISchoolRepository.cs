// ============================================================
// RUTA: Backend/Infrastructure/Repositories/Contract/ISchoolRepository.cs
// ============================================================
using LuminiSchool.Domain.Entities.School;

namespace LuminiSchool.Infrastructure.Repositories.Contract
{
    public interface ISchoolRepository : IGenericRepository<SchoolEntity>
    {
        // Agrega aquí métodos específicos si los necesitas en el futuro,
        // por ejemplo: Task<SchoolEntity?> GetByNitAsync(string nit);
    }
}
