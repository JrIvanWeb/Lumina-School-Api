using LuminiSchool.Domain.Entities.Guardian;
using LuminiSchool.Infrastructure.Repositories.Contract;
using Microsoft.EntityFrameworkCore;

namespace LuminiSchool.Infrastructure.Repositories.Implementation
{
    public class GuardianRepository : GenericRepository<GuardianEntity>, IGuardianRepository
    {
        public GuardianRepository(ApplicationDbContext ctx) : base(ctx) { }

        public async Task<IEnumerable<GuardianEntity>> GetByStudentAsync(Guid sid) =>
            await _db.Where(g => g.Students.Any(s => s.Id == sid))
                     .Include(g => g.Students)
                     .Include(g => g.Parent)
                     .ToListAsync();

        public async Task<GuardianEntity?> GetByDocumentAsync(string documentNumber) =>
            await _db.Include(g => g.Students)
                     .FirstOrDefaultAsync(g => g.DocumentNumber == documentNumber);

        public async Task AssignStudentAsync(Guid gid, Guid sid)
        {
            var g = await _db.Include(x => x.Students).FirstOrDefaultAsync(x => x.Id == gid);
            var s = await _ctx.Students.FindAsync(sid);
            if (g != null && s != null) { g.Students.Add(s); await _ctx.SaveChangesAsync(); }
        }

        public async Task AddOrUpdateAsync(GuardianEntity guardian)
        {
            // Cargar entidad existente con sus estudiantes para evitar duplicate key en GuardianStudents
            var existing = await _db
                .Include(g => g.Students)
                .FirstOrDefaultAsync(g => g.Id == guardian.Id);

            if (existing != null)
            {
                // Actualizar solo campos escalares, sin reemplazar la colección
                _ctx.Entry(existing).CurrentValues.SetValues(guardian);

                // Agregar únicamente los estudiantes que aún no están vinculados
                foreach (var student in guardian.Students)
                {
                    if (!existing.Students.Any(s => s.Id == student.Id))
                    {
                        var trackedStudent = _ctx.ChangeTracker
                            .Entries<Domain.Entities.Student.StudentEntity>()
                            .FirstOrDefault(e => e.Entity.Id == student.Id)?.Entity
                            ?? await _ctx.Students.FindAsync(student.Id);

                        if (trackedStudent != null)
                            existing.Students.Add(trackedStudent);
                    }
                }
            }
            else
            {
                await _db.AddAsync(guardian);
            }

            await _ctx.SaveChangesAsync();
        }
    }
}
