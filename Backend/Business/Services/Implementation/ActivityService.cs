using AutoMapper;
using LuminiSchool.Business.Exceptions;
using LuminiSchool.Business.Services.Contract;
using LuminiSchool.Domain.Entities.Activity;
using LuminiSchool.Domain.Model.Activity.DTOs;
using LuminiSchool.Infrastructure.Repositories.Contract;

namespace LuminiSchool.Business.Services.Implementation
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository _r; private readonly IMapper _m;
        public ActivityService(IActivityRepository r, IMapper m) { _r = r; _m = m; }
        public async Task<IEnumerable<ActivityDto>> GetAllAsync() => _m.Map<IEnumerable<ActivityDto>>(await _r.GetAllAsync());
        public async Task<ActivityDto> GetByIdAsync(Guid id) => _m.Map<ActivityDto>(await _r.GetByIdAsync(id) ?? throw new NotFoundException($"Actividad {id} no encontrada."));
        public async Task<IEnumerable<ActivityDto>> GetByTeacherAsync(Guid tid) => _m.Map<IEnumerable<ActivityDto>>(await _r.GetByTeacherAsync(tid));
        public async Task<ActivityDto> CreateAsync(CreateActivityDto dto) { var e = _m.Map<ActivityEntity>(dto); e.Id = Guid.NewGuid(); return _m.Map<ActivityDto>(await _r.AddAsync(e)); }
        public async Task DeleteAsync(Guid id) { if (!await _r.ExistsAsync(id)) throw new NotFoundException($"Actividad {id} no encontrada."); await _r.DeleteAsync(id); }
        public async Task<ActivitySubmissionDto> SubmitAsync(SubmitActivityDto dto) { var s = new ActivitySubmissionEntity { Id = Guid.NewGuid(), ActivityId = dto.ActivityId, StudentId = dto.StudentId, FileUrl = dto.FileUrl, Comments = dto.Comments, SubmittedAt = DateTime.UtcNow }; return _m.Map<ActivitySubmissionDto>(await _r.AddSubmissionAsync(s)); }
        public async Task<ActivitySubmissionDto> GradeSubmissionAsync(GradeSubmissionDto dto) { var s = await _r.GetSubmissionByStudentAsync(dto.SubmissionId, Guid.Empty) ?? throw new NotFoundException("Entrega no encontrada."); s.Score = dto.Score; s.Feedback = dto.Feedback; s.GradedAt = DateTime.UtcNow; await _r.UpdateSubmissionAsync(s); return _m.Map<ActivitySubmissionDto>(s); }
        public async Task<IEnumerable<ActivitySubmissionDto>> GetSubmissionsAsync(Guid aid) => _m.Map<IEnumerable<ActivitySubmissionDto>>(await _r.GetSubmissionsAsync(aid));
    }
}
