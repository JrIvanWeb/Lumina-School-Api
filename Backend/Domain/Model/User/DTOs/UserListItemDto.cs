// RUTA: Backend/Domain/Model/User/DTOs/UserListItemDto.cs
namespace LuminiSchool.Domain.Model.User.DTOs
{
    public class UserListItemDto
    {
        public Guid          Id             { get; set; }
        public string        FullName       { get; set; } = string.Empty;
        public string        Email          { get; set; } = string.Empty;
        public IList<string> Roles          { get; set; } = new List<string>();
        public bool          IsActive       { get; set; }
        public DateTime      CreatedAt      { get; set; }
        public string?       DocumentNumber { get; set; }
        public bool          HasCredentials { get; set; }
        public string?       Username       { get; set; }
    }
}
