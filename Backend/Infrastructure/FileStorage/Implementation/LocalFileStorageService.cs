using LuminiSchool.Infrastructure.FileStorage.Contract;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace LuminiSchool.Infrastructure.FileStorage.Implementation
{
    public class LocalFileStorageService : IFileStorageService
    {
        private readonly string _basePath;
        private readonly string _baseUrl;

        public LocalFileStorageService(IConfiguration config, IWebHostEnvironment env)
        {
            _basePath = Path.Combine(env.WebRootPath ?? "wwwroot", "uploads");
            _baseUrl  = config["FileStorage:BaseUrl"] ?? "/uploads";
            Directory.CreateDirectory(_basePath);
        }

        public async Task<string> UploadAsync(IFormFile file, string folder)
        {
            var dir = Path.Combine(_basePath, folder);
            Directory.CreateDirectory(dir);
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var fullPath = Path.Combine(dir, fileName);
            using var stream = new FileStream(fullPath, FileMode.Create);
            await file.CopyToAsync(stream);
            return $"{_baseUrl}/{folder}/{fileName}";
        }

        public Task DeleteAsync(string fileUrl)
        {
            var rel  = fileUrl.Replace(_baseUrl, "").TrimStart('/');
            var full = Path.Combine(_basePath, rel);
            if (File.Exists(full)) File.Delete(full);
            return Task.CompletedTask;
        }

        public Task<Stream> DownloadAsync(string fileUrl)
        {
            var rel  = fileUrl.Replace(_baseUrl, "").TrimStart('/');
            Stream s = new FileStream(Path.Combine(_basePath, rel), FileMode.Open, FileAccess.Read);
            return Task.FromResult(s);
        }

        public bool Exists(string fileUrl)
        {
            var rel = fileUrl.Replace(_baseUrl, "").TrimStart('/');
            return File.Exists(Path.Combine(_basePath, rel));
        }
    }
}
