using LuminiSchool.Domain.Model.Enrollment.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LuminiSchool.Business.Services.Contract
{
    public interface IEnrollmentService
    {
        Task<IEnumerable<EnrollmentDto>> GetAllAsync();
        Task<IEnumerable<EnrollmentDto>> GetByStudentAsync(Guid studentId);
        Task<EnrollmentDto> CreateAsync(CreateEnrollmentDto dto);
        Task<FichaMatriculaResponseDto> CreateFichaAsync(CreateFichaMatriculaDto dto);
        Task WithdrawAsync(Guid enrollmentId);
    }
}