using Microsoft.AspNetCore.Identity;

namespace LuminiSchool.Domain.Entities.User
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? ProfilePictureUrl { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }

    public class ApplicationRole : IdentityRole<Guid>
    {
        public string? Description { get; set; }

        public static class Roles
        {
            public const string SuperAdmin = "SuperAdmin";
            public const string Admin      = "Admin";
            public const string Rector     = "Rector";
            public const string Teacher    = "Teacher";
            public const string Student    = "Student";
            public const string Guardian   = "Guardian";
        }
    }
}
