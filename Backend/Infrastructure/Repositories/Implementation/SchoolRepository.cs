// ============================================================
// RUTA: Backend/Infrastructure/Repositories/Implementation/SchoolRepository.cs
// ============================================================
using LuminiSchool.Domain.Entities.School;
using LuminiSchool.Infrastructure.Repositories.Contract;

namespace LuminiSchool.Infrastructure.Repositories.Implementation
{
    public class SchoolRepository : GenericRepository<SchoolEntity>, ISchoolRepository
    {
        public SchoolRepository(ApplicationDbContext ctx) : base(ctx) { }
    }
}
