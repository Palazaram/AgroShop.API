using AgroShop.Core.Shared;
using AgroShop.Core.ValueObjects;
using Microsoft.AspNetCore.Http;
using CSharpFunctionalExtensions;

namespace AgroShop.Application.Interfaces
{
    public interface IProductImageService
    {
        Task<Result<string, Error>> SaveImageAsync(CancellationToken cancellationToken, IFormFile image, Guid subCategoryId, Guid supplierId, string productName);
        Task<Result<string, Error>> MoveImageAsync(CancellationToken cancellationToken, string oldImagePath, Guid subCategoryId, Guid supplierId, string productName);
        void DeleteImage(string imagePath);
    }
}
