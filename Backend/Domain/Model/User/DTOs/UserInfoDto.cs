// RUTA: Backend/Domain/Model/User/DTOs/UserInfoDto.cs
namespace LuminiSchool.Domain.Model.User.DTOs
{
    public class UserInfoDto
    {
        public Guid          Id           { get; set; }
        public string        FirstName    { get; set; } = string.Empty;
        public string        LastName     { get; set; } = string.Empty;
        public string        FullName     { get; set; } = string.Empty;
        public string        Email        { get; set; } = string.Empty;
        public IList<string> Roles        { get; set; } = new List<string>();
        public bool          IsFirstLogin { get; set; }
    }
}
