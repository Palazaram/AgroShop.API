using AgroShop.Application.Interfaces;

namespace AgroShop.API.Services
{
    /// <summary>
    /// Saves uploaded images under wwwroot/images so they're served as-is by the static
    /// files middleware. In Docker, wwwroot/images is mounted as a volume so uploads
    /// survive container recreation - see docker-compose.yml.
    /// </summary>
    public class ImageStorageService : IImageStorageService
    {
        private readonly IWebHostEnvironment _environment;

        public ImageStorageService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        // IWebHostEnvironment.WebRootPath is null when wwwroot doesn't exist yet at host
        // startup (e.g. a fresh clone/container before any image was ever uploaded), so it
        // can't be used directly - fall back to the conventional ContentRootPath/wwwroot.
        private string WebRootPath => string.IsNullOrEmpty(_environment.WebRootPath)
            ? Path.Combine(_environment.ContentRootPath, "wwwroot")
            : _environment.WebRootPath;

        public async Task<string> SaveAsync(IFormFile file, string subfolder, CancellationToken cancellationToken = default)
        {
            var extension = Path.GetExtension(file.FileName);
            var fileName = $"{Guid.CreateVersion7()}{extension}";

            var folderPath = Path.Combine(WebRootPath, "images", subfolder);
            Directory.CreateDirectory(folderPath);

            var filePath = Path.Combine(folderPath, fileName);
            await using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream, cancellationToken);

            return $"/images/{subfolder}/{fileName}";
        }

        public Task DeleteAsync(string? relativePath, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(relativePath))
                return Task.CompletedTask;

            var physicalPath = Path.Combine(
                WebRootPath,
                relativePath.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));

            if (File.Exists(physicalPath))
                File.Delete(physicalPath);

            return Task.CompletedTask;
        }
    }
}
