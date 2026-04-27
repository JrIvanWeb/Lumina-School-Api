namespace LuminiSchool.Domain.Entities.User
{
    public class Permission
    {
        public Guid   Id          { get; set; } = Guid.NewGuid();
        public string Name        { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Module      { get; set; } = string.Empty;
       

        public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    }
}
