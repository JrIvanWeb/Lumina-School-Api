using LuminiSchool.Domain.Entities.User;

namespace LuminiSchool.Infrastructure.Repositories.Contract
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken?> GetActiveAsync(string token);
        Task AddAsync(RefreshToken token);
        Task RevokeAsync(RefreshToken token, string? replacedBy = null);
        Task RevokeAllByUserAsync(Guid userId);
        Task SaveChangesAsync();
    }
}
