using LuminiSchool.Domain.Entities.IcfesSimulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Infrastructure.Repositories.Contract
{
    public interface IIcfesSimulatorRepository : IGenericRepository<IcfesSimulatorEntity>
    {
        Task<IEnumerable<IcfesSimulatorEntity>> GetByStatusAsync(IcfesSimulatorStatus status);
        Task<IEnumerable<IcfesResultEntity>> GetResultsBySimulatorAsync(Guid simulatorId);
        Task<IEnumerable<IcfesResultEntity>> GetResultsByStudentAsync(Guid studentId);
        Task<IcfesResultEntity> AddResultAsync(IcfesResultEntity result);
    }
}
