using LuminiSchool.Domain.Model.IcfesSimulator.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Business.Services.Contract
{
    public interface IIcfesSimulatorService
    {
        Task<IEnumerable<IcfesSimulatorDto>> GetAllAsync();
        Task<IcfesSimulatorDto> GetByIdAsync(Guid id);
        Task<IcfesSimulatorDto> CreateAsync(CreateIcfesSimulatorDto dto, Guid coordinatorId);
        Task ActivateAsync(Guid id);
        Task CompleteAsync(Guid id);
        Task<IcfesResultDto> RegisterResultAsync(CreateIcfesResultDto dto);
        Task<IEnumerable<IcfesResultDto>> GetResultsBySimulatorAsync(Guid simulatorId);
        Task<IEnumerable<IcfesResultDto>> GetResultsByStudentAsync(Guid studentId);
    }
}
