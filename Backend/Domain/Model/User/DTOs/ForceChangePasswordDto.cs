// RUTA: Backend/Domain/Model/User/DTOs/ForceChangePasswordDto.cs
namespace LuminiSchool.Domain.Model.User.DTOs
{
    public record ForceChangePasswordDto(string NewPassword, string ConfirmPassword);
}
