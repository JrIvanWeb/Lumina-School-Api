using LuminiSchool.Domain.Model.User.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Business.Services.Contract
{
    public interface IAuthService
    {
        Task<LoginResponseDto> LoginAsync(LoginDto dto);
        Task<UserInfoDto> RegisterAsync(RegisterUserDto dto);
        Task ChangePasswordAsync(Guid userId, ChangePasswordDto dto);
    }
}
