using LuminiSchool.Domain.Entities.Subject;
using LuminiSchool.Infrastructure.Repositories.Contract;
using Microsoft.EntityFrameworkCore;

namespace LuminiSchool.Infrastructure.Repositories.Implementation
{
    public class SubjectRepository : GenericRepository<SubjectEntity>, ISubjectRepository
    {
        public SubjectRepository(ApplicationDbContext ctx) : base(ctx) { }

        /// <summary>
        /// Devuelve todas las asignaturas incluyendo sus grados asociados.
        /// </summary>
        public new async Task<IEnumerable<SubjectEntity>> GetAllAsync()
            => await _db
                .Include(s => s.Grades)
                .ToListAsync();

        /// <summary>
        /// Devuelve una asignatura por id incluyendo grados y docentes.
        /// </summary>
        public new async Task<SubjectEntity?> GetByIdAsync(Guid id)
            => await _db
                .Include(s => s.Grades)
                .Include(s => s.Teachers)
                .FirstOrDefaultAsync(s => s.Id == id);

        public async Task<IEnumerable<SubjectEntity>> GetByTeacherAsync(Guid tid)
            => await _db
                .Include(s => s.Grades)
                .Where(s => s.Teachers.Any(t => t.Id == tid))
                .ToListAsync();
    }
}
