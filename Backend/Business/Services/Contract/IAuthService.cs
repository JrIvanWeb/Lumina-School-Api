using LuminiSchool.Domain.Model.User.DTOs;

namespace LuminiSchool.Business.Services.Contract
{
    public interface IAuthService
    {
        Task<LoginResponseDto>  LoginAsync(LoginDto dto);
        Task<LoginResponseDto>  RefreshTokenAsync(RefreshTokenRequestDto dto);
        Task                  ChangePasswordAsync(Guid userId, ChangePasswordDto dto);
        Task                    ForceChangePasswordAsync(Guid userId, ForceChangePasswordDto dto);
    }
}
