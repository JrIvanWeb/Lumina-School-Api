using AutoMapper;
using LuminiSchool.Business.Exceptions;
using LuminiSchool.Business.Services.Contract;
using LuminiSchool.Domain.Entities.Teacher;
using LuminiSchool.Domain.Entities.User;
using LuminiSchool.Domain.Model.Teacher.DTOs;
using LuminiSchool.Infrastructure.Email.Contract;
using LuminiSchool.Infrastructure.Repositories.Contract;
using Microsoft.AspNetCore.Identity;

namespace LuminiSchool.Business.Services.Implementation
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository             _repo;
        private readonly IMapper                        _mapper;
        private readonly UserManager<ApplicationUser>   _userManager;
        private readonly IEmailService                  _email;

        public TeacherService(
            ITeacherRepository           repo,
            IMapper                      mapper,
            UserManager<ApplicationUser> userManager,
            IEmailService                email)
        {
            _repo        = repo;
            _mapper      = mapper;
            _userManager = userManager;
            _email       = email;
        }

        // ── Consultas ────────────────────────────────────────────────────────

        public async Task<IEnumerable<TeacherDto>> GetAllAsync()
            => _mapper.Map<IEnumerable<TeacherDto>>(await _repo.GetAllAsync());

        public async Task<TeacherDto> GetByIdAsync(Guid id)
            => _mapper.Map<TeacherDto>(
                await _repo.GetByIdAsync(id)
                ?? throw new NotFoundException($"Docente {id} no encontrado."));

        // ── Crear (también crea ApplicationUser con rol Teacher) ─────────────

        public async Task<TeacherDto> CreateAsync(CreateTeacherDto dto)
        {
            // 1. Validar documento duplicado
            if (await _repo.GetByDocumentAsync(dto.DocumentNumber) != null)
                throw new ConflictException($"Ya existe un docente con documento {dto.DocumentNumber}.");

            // 2. Crear ApplicationUser
            var tempPassword = GenerateTemporaryPassword();
            var user = new ApplicationUser
            {
                Id           = Guid.NewGuid(),
                FirstName    = dto.FirstName,
                LastName     = dto.LastName,
                Email        = dto.Email,
                UserName     = dto.Email,
                IsActive     = true,
                IsFirstLogin = true,
                CreatedAt    = DateTime.UtcNow
            };

            var userResult = await _userManager.CreateAsync(user, tempPassword);
            if (!userResult.Succeeded)
                throw new BusinessException(string.Join(", ", userResult.Errors.Select(e => e.Description)));

            var roleResult = await _userManager.AddToRoleAsync(user, "Teacher");
            if (!roleResult.Succeeded)
                throw new BusinessException("Error al asignar el rol Teacher.");

            // 3. Crear TeacherEntity vinculado al usuario
            var teacher = _mapper.Map<TeacherEntity>(dto);
            teacher.Id     = Guid.NewGuid();
            teacher.UserId = user.Id;

            var saved = await _repo.AddAsync(teacher);

            // 4. Enviar contraseña temporal por correo
            await _email.SendTemporaryPasswordAsync(user.Email!, user.FullName, tempPassword);

            return _mapper.Map<TeacherDto>(saved);
        }

        // ── Actualizar ───────────────────────────────────────────────────────

        public async Task<TeacherDto> UpdateAsync(Guid id, UpdateTeacherDto dto)
        {
            var teacher = await _repo.GetByIdAsync(id)
                          ?? throw new NotFoundException($"Docente {id} no encontrado.");

            _mapper.Map(dto, teacher);
            teacher.UpdatedAt = DateTime.UtcNow;

            // Sincronizar nombre/email en el ApplicationUser si existe
            if (teacher.UserId.HasValue)
            {
                var user = await _userManager.FindByIdAsync(teacher.UserId.Value.ToString());
                if (user != null)
                {
                    if (dto.FirstName != null) user.FirstName = dto.FirstName;
                    if (dto.LastName  != null) user.LastName  = dto.LastName;
                    user.UpdatedAt = DateTime.UtcNow;
                    await _userManager.UpdateAsync(user);
                }
            }

            await _repo.UpdateAsync(teacher);
            return _mapper.Map<TeacherDto>(teacher);
        }

        // ── Eliminar (soft-delete del usuario también) ───────────────────────

        public async Task DeleteAsync(Guid id)
        {
            var teacher = await _repo.GetByIdAsync(id)
                          ?? throw new NotFoundException($"Docente {id} no encontrado.");

            if (teacher.UserId.HasValue)
            {
                var user = await _userManager.FindByIdAsync(teacher.UserId.Value.ToString());
                if (user != null)
                {
                    user.IsActive  = false;
                    user.UpdatedAt = DateTime.UtcNow;
                    await _userManager.UpdateAsync(user);
                }
            }

            await _repo.DeleteAsync(id);
        }

        // ── Toggle activo ────────────────────────────────────────────────────

        public async Task ToggleActiveAsync(Guid id)
        {
            var teacher = await _repo.GetByIdAsync(id)
                          ?? throw new NotFoundException($"Docente {id} no encontrado.");

            teacher.IsActive  = !teacher.IsActive;
            teacher.UpdatedAt = DateTime.UtcNow;
            await _repo.UpdateAsync(teacher);

            // Sincronizar con ApplicationUser
            if (teacher.UserId.HasValue)
            {
                var user = await _userManager.FindByIdAsync(teacher.UserId.Value.ToString());
                if (user != null)
                {
                    user.IsActive  = teacher.IsActive;
                    user.UpdatedAt = DateTime.UtcNow;
                    await _userManager.UpdateAsync(user);
                }
            }
        }

        // ── Materias ─────────────────────────────────────────────────────────

        public async Task AssignSubjectAsync(Guid teacherId, Guid subjectId)
            => await _repo.AssignSubjectAsync(teacherId, subjectId);

        public async Task RemoveSubjectAsync(Guid teacherId, Guid subjectId)
            => await _repo.RemoveSubjectAsync(teacherId, subjectId);

        // ── Helpers ──────────────────────────────────────────────────────────

        private static string GenerateTemporaryPassword()
        {
            const string upper   = "ABCDEFGHJKLMNPQRSTUVWXYZ";
            const string lower   = "abcdefghijkmnpqrstuvwxyz";
            const string digits  = "23456789";
            const string special = "!@#$%";
            const string all     = upper + lower + digits + special;

            var rng   = new Random();
            var chars = new char[12];
            chars[0]  = upper[rng.Next(upper.Length)];
            chars[1]  = lower[rng.Next(lower.Length)];
            chars[2]  = digits[rng.Next(digits.Length)];
            chars[3]  = special[rng.Next(special.Length)];
            for (int i = 4; i < 12; i++)
                chars[i] = all[rng.Next(all.Length)];

            return new string(chars.OrderBy(_ => rng.Next()).ToArray());
        }
    }
}
