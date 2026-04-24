using AutoMapper;
using LuminiSchool.Business.Exceptions;
using LuminiSchool.Business.Services.Contract;
using LuminiSchool.Domain.Entities.Enrollment;
using LuminiSchool.Domain.Model.Enrollment.DTOs;
using LuminiSchool.Infrastructure.Repositories.Contract;


namespace LuminiSchool.Business.Services.Implementation
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IEnrollmentRepository _r; private readonly IMapper _m;
        public EnrollmentService(IEnrollmentRepository r, IMapper m) { _r = r; _m = m; }
        public async Task<IEnumerable<EnrollmentDto>> GetAllAsync() => _m.Map<IEnumerable<EnrollmentDto>>(await _r.GetAllAsync());
        public async Task<EnrollmentDto> CreateAsync(CreateEnrollmentDto dto) { var e = _m.Map<EnrollmentEntity>(dto); e.Id = Guid.NewGuid(); e.EnrollmentDate = DateTime.UtcNow; return _m.Map<EnrollmentDto>(await _r.AddAsync(e)); }
        public async Task<IEnumerable<EnrollmentDto>> GetByStudentAsync(Guid sid) => _m.Map<IEnumerable<EnrollmentDto>>(await _r.GetByStudentAsync(sid));
        public async Task WithdrawAsync(Guid id) { var e = await _r.GetByIdAsync(id) ?? throw new NotFoundException($"Matrícula {id} no encontrada."); e.Status = EnrollmentStatus.Withdrawn; e.WithdrawalDate = DateTime.UtcNow; await _r.UpdateAsync(e); }
    }
}
