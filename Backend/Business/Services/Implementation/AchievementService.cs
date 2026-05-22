using AutoMapper;
using LuminiSchool.Business.Exceptions;
using LuminiSchool.Business.Services.Contract;
using LuminiSchool.Domain.Entities.Achievement;
using LuminiSchool.Domain.Model.Achievement.DTOs;
using LuminiSchool.Infrastructure.Repositories.Contract;

namespace LuminiSchool.Business.Services.Implementation
{
    public class AchievementService : IAchievementService
    {
        private readonly IAchievementRepository _repo;
        private readonly IMapper                _mapper;

        public AchievementService(IAchievementRepository repo, IMapper mapper)
        {
            _repo   = repo;
            _mapper = mapper;
        }

       
        public async Task<IEnumerable<AchievementDto>> GetAllAsync()
        {
            var entities = await _repo.GetAllWithDetailsAsync();
            return _mapper.Map<IEnumerable<AchievementDto>>(entities);
        }

        // ── GetFiltered ──────────────────────────────────────────────────────
        public async Task<IEnumerable<AchievementDto>> GetFilteredAsync(
            Guid?             periodId,
            Guid?             gradeId,
            Guid?             subjectId,
            PerformanceLevel? performance)
        {
            var entities = await _repo.GetFilteredAsync(periodId, gradeId, subjectId, performance);
            return _mapper.Map<IEnumerable<AchievementDto>>(entities);
        }

        // ── GetById ──────────────────────────────────────────────────────────
        public async Task<AchievementDto> GetByIdAsync(Guid id)
        {
            var entity = await _repo.GetByIdWithDetailsAsync(id)
                ?? throw new NotFoundException($"Logro con Id '{id}' no encontrado.");

            return _mapper.Map<AchievementDto>(entity);
        }

        // ── Create ───────────────────────────────────────────────────────────
        public async Task<AchievementDto> CreateAsync(CreateAchievementDto dto)
        {
            ValidateNoteRange(dto.NoteMin, dto.NoteMax);

            var entity = _mapper.Map<AchievementEntity>(dto);
            entity.Id        = Guid.NewGuid();
            entity.IsActive  = true;
            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;

            await _repo.AddAsync(entity);

            var created = await _repo.GetByIdWithDetailsAsync(entity.Id)!;
            return _mapper.Map<AchievementDto>(created!);
        }

        // ── Update ───────────────────────────────────────────────────────────
        public async Task<AchievementDto> UpdateAsync(Guid id, UpdateAchievementDto dto)
        {
            var entity = await _repo.GetByIdAsync(id)
                ?? throw new NotFoundException($"Logro con Id '{id}' no encontrado.");

            ValidateNoteRange(dto.NoteMin, dto.NoteMax);

            // Aplicar cambios manualmente para no pisar Id/CreatedAt/IsActive
            entity.PeriodId    = dto.PeriodId;
            entity.GradeId     = dto.GradeId;
            entity.SubjectId   = dto.SubjectId;
            entity.Performance = dto.Performance;
            entity.NoteMin     = dto.NoteMin;
            entity.NoteMax     = dto.NoteMax;
            entity.Achievement = dto.Achievement.Trim();
            entity.Indicator   = string.IsNullOrWhiteSpace(dto.Indicator) ? null : dto.Indicator.Trim();
            entity.UpdatedAt   = DateTime.UtcNow;

            await _repo.UpdateAsync(entity);

            var updated = await _repo.GetByIdWithDetailsAsync(id)!;
            return _mapper.Map<AchievementDto>(updated!);
        }

        // ── Delete (lógico) ──────────────────────────────────────────────────
        public async Task DeleteAsync(Guid id)
        {
            var entity = await _repo.GetByIdAsync(id)
                ?? throw new NotFoundException($"Logro con Id '{id}' no encontrado.");

            entity.IsActive  = false;
            entity.UpdatedAt = DateTime.UtcNow;
            await _repo.UpdateAsync(entity);
        }

        // ── Helpers ──────────────────────────────────────────────────────────
        private static void ValidateNoteRange(decimal min, decimal max)
        {
            if (min < 0 || min > 5 || max < 0 || max > 5)
                throw new ValidationException("Las notas deben estar entre 0.0 y 5.0.");
            if (min > max)
                throw new ValidationException("La nota mínima no puede ser mayor que la nota máxima.");
        }
    }
}
