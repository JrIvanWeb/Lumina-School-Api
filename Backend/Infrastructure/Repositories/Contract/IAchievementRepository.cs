using LuminiSchool.Domain.Entities.Achievement;

namespace LuminiSchool.Infrastructure.Repositories.Contract
{
    public interface IAchievementRepository : IGenericRepository<AchievementEntity>
    {
        /// <summary>
        /// Devuelve todos los logros con sus relaciones (Period, Grade, Subject)
        /// ya incluidas mediante eager-loading.
        /// </summary>
        Task<IEnumerable<AchievementEntity>> GetAllWithDetailsAsync();

        /// <summary>
        /// Filtra por Período + Grado + Asignatura + Nivel de desempeño.
        /// Cualquiera de los parámetros puede ser null para omitirlo.
        /// </summary>
        Task<IEnumerable<AchievementEntity>> GetFilteredAsync(
            Guid?             periodId,
            Guid?             gradeId,
            Guid?             subjectId,
            PerformanceLevel? performance);

        /// <summary>
        /// Devuelve un logro con todas sus relaciones de navegación cargadas.
        /// </summary>
        Task<AchievementEntity?> GetByIdWithDetailsAsync(Guid id);
    }
}
