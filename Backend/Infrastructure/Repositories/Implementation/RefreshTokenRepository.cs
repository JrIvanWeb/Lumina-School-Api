using LuminiSchool.Domain.Entities.User;
using LuminiSchool.Infrastructure.Repositories.Contract;
using Microsoft.EntityFrameworkCore;

namespace LuminiSchool.Infrastructure.Repositories.Implementation
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly ApplicationDbContext _db;
        public RefreshTokenRepository(ApplicationDbContext db) => _db = db;

        public async Task<RefreshToken?> GetActiveAsync(string token) =>
            await _db.RefreshTokens
                     .Include(t => t.User)
                     .FirstOrDefaultAsync(t => t.Token == token && !t.IsRevoked && t.ExpiresAt > DateTime.UtcNow);

        public async Task AddAsync(RefreshToken token) =>
            await _db.RefreshTokens.AddAsync(token);

        public Task RevokeAsync(RefreshToken token, string? replacedBy = null)
        {
            token.IsRevoked        = true;
            token.ReplacedByToken  = replacedBy;
            return Task.CompletedTask;
        }

        public async Task RevokeAllByUserAsync(Guid userId)
        {
            var tokens = await _db.RefreshTokens
                                  .Where(t => t.UserId == userId && !t.IsRevoked)
                                  .ToListAsync();
            foreach (var t in tokens) t.IsRevoked = true;
        }

        public async Task SaveChangesAsync() => await _db.SaveChangesAsync();
    }
}
