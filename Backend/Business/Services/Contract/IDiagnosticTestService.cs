using LuminiSchool.Domain.Model.DiagnosticTest.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Business.Services.Contract
{
    public interface IDiagnosticTestService
    {
        Task<IEnumerable<DiagnosticTestDto>> GetAllAsync();
        Task<DiagnosticTestDto> GetByIdAsync(Guid id);
        Task<IEnumerable<DiagnosticTestDto>> GetByTeacherAsync(Guid teacherId);
        Task<DiagnosticTestDto> CreateAsync(CreateDiagnosticTestDto dto);
        Task DeleteAsync(Guid id);
        Task<DiagnosticResultDto> RegisterResultAsync(CreateDiagnosticResultDto dto);
        Task<IEnumerable<DiagnosticResultDto>> GetResultsAsync(Guid testId);
    }

}
