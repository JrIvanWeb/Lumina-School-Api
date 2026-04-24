using LuminiSchool.Domain.Model.Achievement.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Business.Services.Contract
{
    public interface IAchievementService
    {
        Task<IEnumerable<AchievementDto>> GetAllAsync();
        Task<IEnumerable<AchievementDto>> GetBySubjectAsync(Guid subjectId);
        Task<AchievementDto> CreateAsync(CreateAchievementDto dto);
        Task DeleteAsync(Guid id);
    }
}
