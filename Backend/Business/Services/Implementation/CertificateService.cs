using AutoMapper;
using LuminiSchool.Business.Services.Contract;
using LuminiSchool.Domain.Entities.Certificate;
using LuminiSchool.Domain.Model.Certificate.DTOs;
using LuminiSchool.Infrastructure.Repositories.Contract;


namespace LuminiSchool.Business.Services.Implementation
{
    public class CertificateService: ICertificateService
    {
        private readonly ICertificateRepository _r; private readonly IMapper _m;
        public CertificateService(ICertificateRepository r, IMapper m) { _r = r; _m = m; }
        public async Task<IEnumerable<CertificateDto>> GetByStudentAsync(Guid sid) => _m.Map<IEnumerable<CertificateDto>>(await _r.GetByStudentAsync(sid));
        public Task<byte[]> GeneratePdfAsync(Guid sid, CertificateType type) => Task.FromResult(Array.Empty<byte>());
    }
}
