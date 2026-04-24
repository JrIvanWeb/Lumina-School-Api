namespace LuminiSchool.Presentation.Dtos
{
    public class FileUploadRequest
    {
        public IFormFile File        { get; set; } = null!;
        public string?   Description { get; set; }
        public string?   Category    { get; set; }
    }

    public class FileUploadResponse
    {
        public string   FileName    { get; set; } = string.Empty;
        public string   Url         { get; set; } = string.Empty;
        public long     SizeBytes   { get; set; }
        public DateTime UploadedAt  { get; set; } = DateTime.UtcNow;
    }
}
