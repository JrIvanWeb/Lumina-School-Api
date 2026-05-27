// RUTA: Backend/Domain/Model/User/DTOs/LoginResponseDto.cs
namespace LuminiSchool.Domain.Model.User.DTOs
{
    public class LoginResponseDto
    {
        public string      Token                  { get; set; } = string.Empty;
        public string      RefreshToken           { get; set; } = string.Empty;
        public DateTime    ExpiresAt              { get; set; }
        public bool        RequiresPasswordChange { get; set; }
        public UserInfoDto User                   { get; set; } = new();
    }
}
