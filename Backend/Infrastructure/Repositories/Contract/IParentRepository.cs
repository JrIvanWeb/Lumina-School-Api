using LuminiSchool.Domain.Entities.Parent;

namespace LuminiSchool.Infrastructure.Repositories.Contract
{
    public interface IParentRepository : IGenericRepository<ParentEntity>
    {
        Task<ParentEntity?> GetByDocumentAsync(string documentNumber);
    }
}
