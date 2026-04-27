using LuminiSchool.Domain.Entities.User;

namespace LuminiSchool.Infrastructure.JWT.Contract
{
    public interface IJwtService
    {
        string GenerateToken(ApplicationUser user, IList<string> roles, IList<string> permissions);
        string GenerateRefreshToken();
    }
}
