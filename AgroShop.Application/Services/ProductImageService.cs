using AgroShop.Application.Interfaces;
using AgroShop.Core.Entities;
using AgroShop.Core.Shared;
using AgroShop.Core.ValueObjects;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace AgroShop.Application.Services
{
    public class ProductImageService : IProductImageService
    {
        private readonly ICategoryService _categoryService;
        private readonly ISupplierService _supplierService;
        private readonly ISubCategoryService _subCategoryService;

        private readonly string _rootPath;
        private readonly string _uploadsPath;

        public ProductImageService(ICategoryService categoryService, ISupplierService supplierService, ISubCategoryService subCategoryService, IWebHostEnvironment env)
        {
            _categoryService = categoryService;
            _supplierService = supplierService;
            _subCategoryService = subCategoryService;
            _rootPath = env.WebRootPath;
            _uploadsPath = Path.Combine(env.WebRootPath, "uploads");
        }

        public void DeleteImage(string imagePath)
        {
            throw new NotImplementedException();
        }

        public Task<Result<string, Error>> MoveImageAsync(CancellationToken cancellationToken, string oldImagePath, Guid subCategoryId, Guid supplierId, string productName)
        {
            throw new NotImplementedException();
        }

        public Task<Result<string, Error>> SaveImageAsync(CancellationToken cancellationToken, IFormFile image, Guid subCategoryId, Guid supplierId, string productName)
        {
            throw new NotImplementedException();
        }



        //public async Task<Result<string>> SaveImageAsync(CancellationToken cancellationToken, IFormFile image, SubCategoryId subCategoryId, SupplierId supplierId, string productName)
        //{
        //    cancellationToken.ThrowIfCancellationRequested();

        //    var (supplierResult, subCategoryResult) = await GetSupplierAndSubCategoryAsync(supplierId, subCategoryId, cancellationToken);

        //    if (!supplierResult.IsSuccess)
        //        return Result<string>.Failure(supplierResult.Error);
        //    if (supplierResult.Data == null)
        //        return Result<string>.Failure(SupplierErrors.NotFound(supplierId));

        //    if (!subCategoryResult.IsSuccess)
        //        return Result<string>.Failure(subCategoryResult.Error);
        //    if (subCategoryResult.Data == null)
        //        return Result<string>.Failure(SubCategoryErrors.NotFound(subCategoryId));

        //    var categoryResult = await GetCategoryAsync(subCategoryResult.Data!.CategoryId, cancellationToken);

        //    if (!categoryResult.IsSuccess)
        //        return Result<string>.Failure(categoryResult.Error);
        //    if (categoryResult.Data == null)
        //        return Result<string>.Failure(CategoryErrors.NotFound(subCategoryResult.Data!.CategoryId));

        //    var supplierName = supplierResult.Data.Name.Trim();
        //    var subCategoryName = subCategoryResult.Data.Name.Trim();
        //    var categoryName = categoryResult.Data.Name.Trim();

        //    string folder = Path.Combine(_uploadsPath, supplierName, categoryName, subCategoryName, productName);
        //    Directory.CreateDirectory(folder);

        //    string fileName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
        //    string fullPath = Path.Combine(folder, fileName);

        //    await using var fileStream = new FileStream(fullPath, FileMode.Create);
        //    await image.CopyToAsync(fileStream, cancellationToken);

        //    return Result<string>.Success($"/uploads/{supplierName}/{categoryName}/{subCategoryName}/{productName}/{fileName}");
        //}

        //public async Task<Result<string>> MoveImageAsync(CancellationToken cancellationToken, string oldImagePath, SubCategoryId subCategoryId, SupplierId supplierId, string productName)
        //{
        //    cancellationToken.ThrowIfCancellationRequested();

        //    var (supplierResult, subCategoryResult) = await GetSupplierAndSubCategoryAsync(supplierId, subCategoryId, cancellationToken);

        //    if (!supplierResult.IsSuccess)
        //        return Result<string>.Failure(supplierResult.Error);
        //    if (supplierResult.Data == null)
        //        return Result<string>.Failure(SupplierErrors.NotFound(supplierId));

        //    if (!subCategoryResult.IsSuccess)
        //        return Result<string>.Failure(subCategoryResult.Error);
        //    if (subCategoryResult.Data == null)
        //        return Result<string>.Failure(SubCategoryErrors.NotFound(subCategoryId));

        //    var categoryResult = await GetCategoryAsync(subCategoryResult.Data!.CategoryId, cancellationToken);

        //    if (!categoryResult.IsSuccess)
        //        return Result<string>.Failure(categoryResult.Error);
        //    if (categoryResult.Data == null)
        //        return Result<string>.Failure(CategoryErrors.NotFound(subCategoryResult.Data!.CategoryId));

        //    string oldFullPath = Path.Combine(_rootPath, oldImagePath.TrimStart('/')).Replace('/', Path.DirectorySeparatorChar);
        //    if (!File.Exists(oldFullPath))
        //        return Result<string>.Failure(FileErrors.OldProductImageDoesntExist);

        //    var supplierName = supplierResult.Data.Name.Trim();
        //    var subCategoryName = subCategoryResult.Data.Name.Trim();
        //    var categoryName = categoryResult.Data.Name.Trim();

        //    string newFolder = Path.Combine(_uploadsPath, supplierName, categoryName, subCategoryName, productName);
        //    Directory.CreateDirectory(newFolder);

        //    string fileName = Path.GetFileName(oldFullPath);
        //    string newFullPath = Path.Combine(newFolder, fileName);
        //    File.Move(oldFullPath, newFullPath);

        //    DeleteEmptyDirectoriesUpToUploads(oldImagePath);

        //    return Result<string>.Success($"/uploads/{supplierName}/{categoryName}/{subCategoryName}/{productName}/{fileName}");
        //}

        //public void DeleteImage(string imagePath)
        //{
        //    if (string.IsNullOrWhiteSpace(imagePath)) return;

        //    string fullPath = Path.Combine(_rootPath, imagePath.TrimStart('/')).Replace('/', Path.DirectorySeparatorChar);

        //    if (File.Exists(fullPath))
        //    {
        //        File.Delete(fullPath);
        //        DeleteEmptyDirectoriesUpToUploads(imagePath);
        //    }
        //}

        //private void DeleteEmptyDirectoriesUpToUploads(string imagePath)
        //{
        //    if (string.IsNullOrWhiteSpace(imagePath)) return;

        //    string fullPath = Path.Combine(_rootPath, imagePath.TrimStart('/')).Replace('/', Path.DirectorySeparatorChar);
        //    string? currentDir = Path.GetDirectoryName(fullPath);

        //    while (!string.IsNullOrWhiteSpace(currentDir) && currentDir.StartsWith(_uploadsPath))
        //    {
        //        if (Directory.Exists(currentDir) && Directory.GetFileSystemEntries(currentDir).Length == 0)
        //        {
        //            Directory.Delete(currentDir);
        //            currentDir = Path.GetDirectoryName(currentDir);
        //        }
        //        else break;
        //    }
        //}

        //private async Task<(Result<Supplier?> supplier, Result<SubCategory?> subCategory)> GetSupplierAndSubCategoryAsync(SupplierId supplierId, SubCategoryId subCategoryId, CancellationToken cancellationToken)
        //{
        //    var supplierTask = _supplierService.GetSupplierByIdAsync(supplierId, cancellationToken, asNoTracking: true);
        //    var subCategoryTask = _subCategoryService.GetSubCategoryByIdAsync(subCategoryId, cancellationToken, asNoTracking: true);

        //    await Task.WhenAll(supplierTask, subCategoryTask);

        //    return (supplierTask.Result, subCategoryTask.Result);
        //}

        //private async Task<Result<Category?>> GetCategoryAsync(CategoryId categoryId, CancellationToken cancellationToken)
        //{
        //    var category = await _categoryService.GetCategoryByIdAsync(categoryId, cancellationToken, asNoTracking: true);
        //    return category;
        //}
    }
}
