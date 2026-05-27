
using AutoMapper;
using LuminiSchool.Business.Exceptions;
using LuminiSchool.Business.Services.Contract;
using LuminiSchool.Domain.Entities.School;
using LuminiSchool.Domain.Model.School.DTOs;
using LuminiSchool.Infrastructure.Repositories.Contract;
using System.Text.Json;

namespace LuminiSchool.Business.Services.Implementation
{
    public class SchoolService : ISchoolService
    {
        private readonly ISchoolRepository _repo;
        private readonly IMapper _mapper;

        public SchoolService(ISchoolRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SchoolDto>> GetAllAsync()
        {
            var entities = await _repo.GetAllAsync();
            return entities.Select(MapToDto);
        }

        public async Task<SchoolDto> GetByIdAsync(Guid id)
        {
            var entity = await _repo.GetByIdAsync(id)
                ?? throw new NotFoundException($"Colegio {id} no encontrado.");
            return MapToDto(entity);
        }

        public async Task<SchoolDto> CreateAsync(CreateSchoolDto dto)
        {
            var entity = new SchoolEntity
            {
                Id = Guid.NewGuid(),
                SchoolName = dto.SchoolName,
                SchoolNit = dto.SchoolNit,
                City = dto.City,
                Department = dto.Department,
                Phone = dto.Phone,
                Email = dto.Email,
                Address = dto.Address,
                LogoUrl = dto.LogoUrl,
                MinGrade = dto.MinGrade,
                MaxGrade = dto.MaxGrade,
                PassingGrade = dto.PassingGrade,
                NumPeriods = dto.NumPeriods,
                CurrentYear = dto.CurrentYear,
                ModulesJson = JsonSerializer.Serialize(dto.Modules),
                RolesJson = JsonSerializer.Serialize(dto.Roles),
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            var created = await _repo.AddAsync(entity);
            return MapToDto(created);
        }

        public async Task DeleteAsync(Guid id)
        {
            if (!await _repo.ExistsAsync(id))
                throw new NotFoundException($"Colegio {id} no encontrado.");
            await _repo.DeleteAsync(id);
        }

        // ── Helpers ──────────────────────────────────────────────────────────
        private static SchoolDto MapToDto(SchoolEntity e)
        {
            Dictionary<string, bool> modules = new();
            Dictionary<string, bool> roles = new();

            try { modules = JsonSerializer.Deserialize<Dictionary<string, bool>>(e.ModulesJson) ?? new(); } catch { }
            try { roles = JsonSerializer.Deserialize<Dictionary<string, bool>>(e.RolesJson) ?? new(); } catch { }

            return new SchoolDto
            {
                Id = e.Id,
                SchoolName = e.SchoolName,
                SchoolNit = e.SchoolNit,
                City = e.City,
                Department = e.Department,
                Phone = e.Phone,
                Email = e.Email,
                Address = e.Address,
                LogoUrl = e.LogoUrl,
                MinGrade = e.MinGrade,
                MaxGrade = e.MaxGrade,
                PassingGrade = e.PassingGrade,
                NumPeriods = e.NumPeriods,
                CurrentYear = e.CurrentYear,
                Modules = modules,
                Roles = roles,
                IsActive = e.IsActive,
                CreatedAt = e.CreatedAt
            };
        }
    }
}
