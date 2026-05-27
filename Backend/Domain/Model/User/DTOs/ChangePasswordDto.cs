namespace LuminiSchool.Domain.Model.User.DTOs
{
    public record ChangePasswordDto(string CurrentPassword, string NewPassword);
}
