using LuminiSchool.Domain.Model.GradeRecord.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Business.Services.Contract
{
    public interface IGradeRecordService
    {
        Task<IEnumerable<GradeRecordDto>> GetByStudentAndPeriodAsync(Guid studentId, Guid periodId);
        Task<IEnumerable<GradeRecordDto>> GetBySubjectAndPeriodAsync(Guid subjectId, Guid periodId);
        Task<GradeRecordDto> RegisterAsync(CreateGradeRecordDto dto);
        Task<GradeRecordDto> UpdateAsync(Guid id, UpdateGradeRecordDto dto);
        Task<decimal?> GetAverageAsync(Guid studentId, Guid periodId);
    }
}
