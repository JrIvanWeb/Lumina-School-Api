using LuminiSchool.Domain.Entities.Subject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Infrastructure.Repositories.Contract
{
    public interface ISubjectRepository : IGenericRepository<SubjectEntity>
    {
        Task<IEnumerable<SubjectEntity>> GetByTeacherAsync(Guid teacherId);
    }
}
