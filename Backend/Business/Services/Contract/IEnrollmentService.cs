using LuminiSchool.Domain.Model.Enrollment.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LuminiSchool.Business.Services.Contract
{
    public interface IEnrollmentService
    {
        Task<IEnumerable<EnrollmentDto>> GetAllAsync();
        Task<EnrollmentDto> GetByIdAsync(Guid enrollmentId);
        Task<CreateFichaMatriculaDto> GetFichaAsync(Guid enrollmentId);
        Task<IEnumerable<EnrollmentDto>> GetByStudentAsync(Guid studentId);
        Task<EnrollmentDto> CreateAsync(CreateEnrollmentDto dto);
        Task<EnrollmentDto> UpdateAsync(Guid enrollmentId, UpdateEnrollmentDto dto);
        Task<FichaMatriculaResponseDto> CreateFichaAsync(CreateFichaMatriculaDto dto);
        Task ActivateAsync(Guid enrollmentId);
        Task WithdrawAsync(Guid enrollmentId);
        Task DeleteAsync(Guid enrollmentId);
    }
}
