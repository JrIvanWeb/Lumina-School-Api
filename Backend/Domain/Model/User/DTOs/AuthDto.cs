namespace LuminiSchool.Domain.Model.User.DTOs
{

    public record LoginDto(string Email, string Password);

    public record CreateUserDto(string FirstName, string LastName, string Email, string Role);
    public record UpdateUserDto(string FirstName, string LastName, string Role);
    public record ForceChangePasswordDto(string NewPassword, string ConfirmPassword);

    public record ChangePasswordDto(string CurrentPassword, string NewPassword);

    public record RefreshTokenRequestDto(string RefreshToken);


    public class LoginResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
       
        public bool RequiresPasswordChange { get; set; }
        public UserInfoDto User { get; set; } = new();
    }

    public class UserInfoDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public IList<string> Roles { get; set; } = new List<string>();
        public bool IsFirstLogin { get; set; }
    }

    public class UserPermissionsDto
    {
        public Guid UserId { get; set; }
        public IList<string> Roles { get; set; } = new List<string>();
        public IList<string> Permissions { get; set; } = new List<string>();
    }

    public class PermissionDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string Module { get; set; } = string.Empty;
    }

    public class UserListItemDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public IList<string> Roles { get; set; } = new List<string>();
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
