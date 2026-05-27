// RUTA: Backend/Domain/Model/User/DTOs/UserPermissionsDto.cs
namespace LuminiSchool.Domain.Model.User.DTOs
{
    public class UserPermissionsDto
    {
        public Guid          UserId      { get; set; }
        public IList<string> Roles       { get; set; } = new List<string>();
        public IList<string> Permissions { get; set; } = new List<string>();
    }
}
