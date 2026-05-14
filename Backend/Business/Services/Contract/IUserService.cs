using LuminiSchool.Domain.Model.User.DTOs;

namespace LuminiSchool.Business.Services.Contract
{
    public interface IUserService
    {
        Task<UserInfoDto>           CreateUserAsync(CreateUserDto dto);
        Task<UserInfoDto>           UpdateUserAsync(Guid userId, UpdateUserDto dto);
        Task                        DeleteUserAsync(Guid userId);
        Task<IList<UserListItemDto>> GetAllUsersAsync();
        Task                        ToggleActiveAsync(Guid userId);
    }
}
