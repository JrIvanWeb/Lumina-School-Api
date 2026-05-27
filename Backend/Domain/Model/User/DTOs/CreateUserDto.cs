// RUTA: Backend/Domain/Model/User/DTOs/CreateUserDto.cs
namespace LuminiSchool.Domain.Model.User.DTOs
{
    public class CreateUserDto
    {
        public string  FirstName      { get; set; } = string.Empty;
        public string  LastName       { get; set; } = string.Empty;
        public string  Email          { get; set; } = string.Empty;
        public string  Role           { get; set; } = string.Empty;
        public string? DocumentNumber { get; set; }
    }
}
