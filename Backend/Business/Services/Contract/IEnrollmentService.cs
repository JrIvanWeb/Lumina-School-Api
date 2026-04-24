using LuminiSchool.Domain.Model.Enrollment.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Business.Services.Contract
{
    public interface IEnrollmentService
    {
        Task<IEnumerable<EnrollmentDto>> GetAllAsync();
        Task<EnrollmentDto> CreateAsync(CreateEnrollmentDto dto);
        Task<IEnumerable<EnrollmentDto>> GetByStudentAsync(Guid studentId);
        Task WithdrawAsync(Guid enrollmentId);
    }
}
