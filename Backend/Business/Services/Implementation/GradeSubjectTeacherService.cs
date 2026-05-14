using AutoMapper;
using LuminiSchool.Business.Exceptions;
using LuminiSchool.Business.Services.Contract;
using LuminiSchool.Domain.Entities.GradeSubjectTeacher;
using LuminiSchool.Domain.Model.GradeSubjectTeacher.DTOs;
using LuminiSchool.Infrastructure.Repositories.Contract;

namespace LuminiSchool.Business.Services.Implementation
{
    public class GradeSubjectTeacherService : IGradeSubjectTeacherService
    {
        private readonly IGradeSubjectTeacherRepository _r;
        private readonly IMapper _m;

        public GradeSubjectTeacherService(IGradeSubjectTeacherRepository r, IMapper m)
        {
            _r = r;
            _m = m;
        }

        public async Task<IEnumerable<GradeSubjectTeacherDto>> GetAllAsync() =>
            _m.Map<IEnumerable<GradeSubjectTeacherDto>>(await _r.GetAllAsync());

        public async Task<IEnumerable<GradeSubjectTeacherDto>> GetByGradeAsync(Guid gradeId) =>
            _m.Map<IEnumerable<GradeSubjectTeacherDto>>(await _r.GetByGradeAsync(gradeId));

        public async Task<GradeSubjectTeacherDto> GetByIdAsync(Guid id) =>
            _m.Map<GradeSubjectTeacherDto>(await _r.GetByIdAsync(id)
                ?? throw new NotFoundException($"Asignación {id} no encontrada."));

        public async Task<GradeSubjectTeacherDto> CreateAsync(CreateGradeSubjectTeacherDto dto)
        {
            if (await _r.ExistsAsync(dto.GradeId, dto.SubjectId, dto.TeacherId))
                throw new ConflictException("Esta asignación ya existe para el grado, materia y profesor indicados.");

            var entity = _m.Map<GradeSubjectTeacherEntity>(dto);
            entity.Id = Guid.NewGuid();
            return _m.Map<GradeSubjectTeacherDto>(await _r.AddAsync(entity));
        }

        public async Task<GradeSubjectTeacherDto> UpdateAsync(Guid id, UpdateGradeSubjectTeacherDto dto)
        {
            var entity = await _r.GetByIdAsync(id)
                ?? throw new NotFoundException($"Asignación {id} no encontrada.");

            entity.TeacherId = dto.TeacherId;
            await _r.UpdateAsync(entity);
            return _m.Map<GradeSubjectTeacherDto>(await _r.GetByIdAsync(id));
        }

        public async Task DeleteAsync(Guid id)
        {
            if (await _r.GetByIdAsync(id) == null)
                throw new NotFoundException($"Asignación {id} no encontrada.");
            await _r.DeleteAsync(id);
        }
    }
}
