using LuminiSchool.Domain.Entities.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Infrastructure.Repositories.Contract
{
    public interface IObserverRepository : IGenericRepository<ObserverEntity>
    {
        Task<IEnumerable<ObserverEntity>> GetByStudentAsync(Guid studentId);
    }
}
