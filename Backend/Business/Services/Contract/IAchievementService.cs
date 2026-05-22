using LuminiSchool.Domain.Entities.Achievement;
using LuminiSchool.Domain.Model.Achievement.DTOs;

namespace LuminiSchool.Business.Services.Contract
{
    public interface IAchievementService
    {
        /// <summary>Devuelve todos los logros activos con sus relaciones.</summary>
        Task<IEnumerable<AchievementDto>> GetAllAsync();

        /// <summary>
        /// Filtra logros. Todos los parámetros son opcionales (null = sin filtro).
        /// </summary>
        Task<IEnumerable<AchievementDto>> GetFilteredAsync(
            Guid?             periodId,
            Guid?             gradeId,
            Guid?             subjectId,
            PerformanceLevel? performance);

        /// <summary>Devuelve un logro por su Id (lanza NotFoundException si no existe).</summary>
        Task<AchievementDto> GetByIdAsync(Guid id);

        /// <summary>Crea un nuevo logro y lo devuelve con sus relaciones enriquecidas.</summary>
        Task<AchievementDto> CreateAsync(CreateAchievementDto dto);

        /// <summary>Actualiza un logro existente (lanza NotFoundException si no existe).</summary>
        Task<AchievementDto> UpdateAsync(Guid id, UpdateAchievementDto dto);

        /// <summary>Eliminación lógica (IsActive = false).</summary>
        Task DeleteAsync(Guid id);
    }
}
