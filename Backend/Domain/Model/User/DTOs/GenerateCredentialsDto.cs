// RUTA: Backend/Domain/Model/User/DTOs/GenerateCredentialsDto.cs
namespace LuminiSchool.Domain.Model.User.DTOs
{
    public class GenerateCredentialsDto
    {
        public string DocumentNumber { get; set; } = string.Empty;
        public bool   SendByEmail    { get; set; } = true;
    }
}
