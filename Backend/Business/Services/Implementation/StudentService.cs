using AutoMapper;
using LuminiSchool.Business.Exceptions;
using LuminiSchool.Business.Services.Contract;
using LuminiSchool.Domain.Entities.Student;
using LuminiSchool.Domain.Model.Student.DTOs;
using LuminiSchool.Infrastructure.Repositories.Contract;

namespace LuminiSchool.Business.Services.Implementation
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _r; private readonly IMapper _m;
        public StudentService(IStudentRepository r, IMapper m) { _r = r; _m = m; }
        public async Task<IEnumerable<StudentDto>> GetAllAsync() => _m.Map<IEnumerable<StudentDto>>(await _r.GetAllAsync());
        public async Task<StudentDto> GetByIdAsync(Guid id) => _m.Map<StudentDto>(await _r.GetByIdAsync(id) ?? throw new NotFoundException($"Estudiante {id} no encontrado."));
        public async Task<StudentDto> CreateAsync(CreateStudentDto dto) { if (await _r.GetByDocumentAsync(dto.DocumentNumber) != null) throw new BusinessException($"Ya existe un estudiante con documento {dto.DocumentNumber}."); var e = _m.Map<StudentEntity>(dto); e.Id = Guid.NewGuid(); return _m.Map<StudentDto>(await _r.AddAsync(e)); }
        public async Task<StudentDto> UpdateAsync(Guid id, UpdateStudentDto dto) { var e = await _r.GetByIdAsync(id) ?? throw new NotFoundException($"Estudiante {id} no encontrado."); _m.Map(dto, e); e.UpdatedAt = DateTime.UtcNow; await _r.UpdateAsync(e); return _m.Map<StudentDto>(e); }
        public async Task DeleteAsync(Guid id) { if (!await _r.ExistsAsync(id)) throw new NotFoundException($"Estudiante {id} no encontrado."); await _r.DeleteAsync(id); }
        public async Task<IEnumerable<StudentDto>> GetBirthdaysTodayAsync() => _m.Map<IEnumerable<StudentDto>>(await _r.GetBirthdaysTodayAsync());
        public async Task<IEnumerable<StudentDto>> GetByGradeAsync(Guid gradeId) => _m.Map<IEnumerable<StudentDto>>(await _r.GetByGradeAsync(gradeId));
    }
}
