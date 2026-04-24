using Microsoft.AspNetCore.Http;

namespace LuminiSchool.Infrastructure.FileStorage.Contract
{
    public interface IFileStorageService
    {
        Task<string> UploadAsync(IFormFile file, string folder);
        Task DeleteAsync(string fileUrl);
        Task<Stream> DownloadAsync(string fileUrl);
        bool Exists(string fileUrl);
    }
}
