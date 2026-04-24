using LuminiSchool.Domain.Model.Guardian.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Business.Services.Contract
{
    public interface IGuardianService
    {
        Task<IEnumerable<GuardianDto>> GetAllAsync();
        Task<GuardianDto> GetByIdAsync(Guid id);
        Task<GuardianDto> CreateAsync(CreateGuardianDto dto);
        Task<GuardianDto> UpdateAsync(Guid id, UpdateGuardianDto dto);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<GuardianDto>> GetByStudentAsync(Guid studentId);
        Task AssignStudentAsync(Guid guardianId, Guid studentId);
    }
}
