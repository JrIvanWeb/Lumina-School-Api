// RUTA: Backend/Domain/Model/User/DTOs/GenerateCredentialsResultDto.cs
namespace LuminiSchool.Domain.Model.User.DTOs
{
    public class GenerateCredentialsResultDto
    {
        public string  Username          { get; set; } = string.Empty;
        public string? TemporaryPassword { get; set; }
        public bool    EmailSent         { get; set; }
    }
}
