using Microsoft.AspNetCore.Http;

namespace AgroShop.Application.Interfaces
{
    public interface IImageStorageService
    {
        /// <summary>
        /// Saves the file under wwwroot/images/{subfolder} and returns its public relative path
        /// (e.g. "/images/categories/{file}"), ready to be stored on the entity and served as-is.
        /// </summary>
        Task<string> SaveAsync(IFormFile file, string subfolder, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a previously saved file given the relative path SaveAsync returned.
        /// No-ops if the path is null/empty or the file no longer exists.
        /// </summary>
        Task DeleteAsync(string? relativePath, CancellationToken cancellationToken = default);
    }
}
