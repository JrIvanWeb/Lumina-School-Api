using LuminiSchool.Domain.Entities.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace LuminiSchool.Infrastructure.Seed
{
    /// <summary>
    /// Crea roles y el primer SuperAdmin al arrancar la aplicación.
    /// Solo actúa si no existen registros previos.
    /// </summary>
    public class DbSeeder
    {
        private readonly UserManager<ApplicationUser>  _um;
        private readonly RoleManager<ApplicationRole> _rm;
        private readonly IConfiguration               _cfg;
        private readonly ILogger<DbSeeder>            _logger;

        public DbSeeder(
            UserManager<ApplicationUser>  um,
            RoleManager<ApplicationRole> rm,
            IConfiguration               cfg,
            ILogger<DbSeeder>            logger)
        {
            _um     = um;
            _rm     = rm;
            _cfg    = cfg;
            _logger = logger;
        }

        public async Task SeedAsync()
        {
            // 1. Crear roles
            foreach (var role in ApplicationRole.Roles.All)
            {
                if (!await _rm.RoleExistsAsync(role))
                {
                    await _rm.CreateAsync(new ApplicationRole { Name = role });
                    _logger.LogInformation("Rol creado: {Role}", role);
                }
            }

            // 2. Crear SuperAdmin inicial si no existe ningún usuario
            if (_um.Users.Any()) return;

            var email    = _cfg["Seed:SuperAdminEmail"] ?? "superadmin@luminischool.edu.co";
            var password = _cfg["Seed:SuperAdminPassword"] ?? "Admin@2024!";

            var superAdmin = new ApplicationUser
            {
                Id           = Guid.NewGuid(),
                FirstName    = "Super",
                LastName     = "Admin",
                Email        = email,
                UserName     = email,
                IsActive     = true,
                IsFirstLogin = false,   // El superadmin inicial no necesita cambiar pwd
                CreatedAt    = DateTime.UtcNow
            };

            var result = await _um.CreateAsync(superAdmin, password);
            if (!result.Succeeded)
            {
                _logger.LogError("Error creando SuperAdmin: {Errors}",
                    string.Join(", ", result.Errors.Select(e => e.Description)));
                return;
            }

            await _um.AddToRoleAsync(superAdmin, ApplicationRole.Roles.SuperAdmin);
            _logger.LogInformation("SuperAdmin inicial creado: {Email}", email);
        }
    }
}
