using LuminiSchool.Domain.Model.Observer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Business.Services.Contract
{
    public interface IObserverService
    {
        Task<IEnumerable<ObserverDto>> GetByStudentAsync(Guid studentId);
        Task<ObserverDto> CreateAsync(CreateObserverDto dto);
        Task DeleteAsync(Guid id);
    }
}
