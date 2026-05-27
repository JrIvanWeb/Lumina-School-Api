// RUTA: Backend/Domain/Model/User/DTOs/PermissionDto.cs
namespace LuminiSchool.Domain.Model.User.DTOs
{
    public class PermissionDto
    {
        public Guid   Id          { get; set; }
        public string Name        { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string Module      { get; set; } = string.Empty;
    }
}
