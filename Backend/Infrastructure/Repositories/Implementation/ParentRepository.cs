using LuminiSchool.Domain.Entities.Parent;
using LuminiSchool.Infrastructure.Repositories.Contract;
using Microsoft.EntityFrameworkCore;

namespace LuminiSchool.Infrastructure.Repositories.Implementation
{
    public class ParentRepository : GenericRepository<ParentEntity>, IParentRepository
    {
        public ParentRepository(ApplicationDbContext ctx) : base(ctx) { }

        public async Task<ParentEntity?> GetByDocumentAsync(string documentNumber) =>
            await _db.Include(p => p.Students)
                     .FirstOrDefaultAsync(p => p.DocumentNumber == documentNumber);
    }
}
