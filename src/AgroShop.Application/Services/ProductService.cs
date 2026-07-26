using AgroShop.Application.Dto.ProductDto;
using AgroShop.Application.Interfaces;
using AgroShop.Application.Mappers;
using AgroShop.Core.Entities;
using AgroShop.Core.Interfaces;
using AgroShop.Core.Shared;
using CSharpFunctionalExtensions;

namespace AgroShop.Application.Services
{
    public class ProductService : IProductService
    {
        private const string ImagesSubfolder = "products";

        // Backstop only - Add/Update/Delete below invalidate this explicitly.
        private const string ProductsCacheKey = "products:all";
        private static readonly TimeSpan ProductsCacheDuration = TimeSpan.FromMinutes(15);

        private readonly IProductRepository _productRepository;
        private readonly ISubCategoryRepository _subCategoryRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageStorageService _imageStorageService;
        private readonly ICacheService _cacheService;

        public ProductService(
            IProductRepository productRepository,
            ISubCategoryRepository subCategoryRepository,
            ISupplierRepository supplierRepository,
            IUnitOfWork unitOfWork,
            IImageStorageService imageStorageService,
            ICacheService cacheService)
        {
            _productRepository = productRepository;
            _subCategoryRepository = subCategoryRepository;
            _supplierRepository = supplierRepository;
            _unitOfWork = unitOfWork;
            _imageStorageService = imageStorageService;
            _cacheService = cacheService;
        }

        public async Task<Result<IEnumerable<ProductDto>, Error>> GetProductsAsync(bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var cached = await _cacheService.GetAsync<List<ProductDto>>(ProductsCacheKey, cancellationToken);
            if (cached != null)
                return Result.Success<IEnumerable<ProductDto>, Error>(cached);

            var products = await _productRepository.GetProductsAsync(asNoTracking, cancellationToken);
            var productDtos = products.ToDto().OrderBy(p => p.Name).ToList();

            await _cacheService.SetAsync(ProductsCacheKey, productDtos, ProductsCacheDuration, cancellationToken);

            return Result.Success<IEnumerable<ProductDto>, Error>(productDtos);
        }

        public async Task<Result<ProductDto, Error>> GetProductByIdAsync(string id, bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (!Guid.TryParse(id, out var productId))
                return Result.Failure<ProductDto, Error>(Errors.General.IncorrectGuidError());

            var product = await _productRepository.GetProductByIdAsync(productId, asNoTracking, cancellationToken);

            if (product == null)
                return Result.Failure<ProductDto, Error>(Errors.Product.ProductIsNullById());

            return Result.Success<ProductDto, Error>(product.ToDto());
        }

        public async Task<UnitResult<Error>> AddAsync(AddProductDto productDto, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var subCategory = await _subCategoryRepository.GetSubCategoryByIdAsync(productDto.SubCategoryId, cancellationToken: cancellationToken);
            if (subCategory == null)
                return UnitResult.Failure(Errors.SubCategory.SubCategoryIsNullById());

            var supplier = await _supplierRepository.GetSupplierByIdAsync(productDto.SupplierId, cancellationToken: cancellationToken);
            if (supplier == null)
                return UnitResult.Failure(Errors.Supplier.SupplierIsNullById());

            var imagePath = await _imageStorageService.SaveAsync(productDto.Image, ImagesSubfolder, cancellationToken);

            var productResult = Product.Create(
                productDto.Name,
                productDto.Description,
                productDto.Price,
                productDto.Sku,
                productDto.StockQuantity,
                productDto.SubCategoryId,
                productDto.SupplierId,
                imagePath);

            if (productResult.IsFailure)
                return UnitResult.Failure(productResult.Error);

            await _productRepository.AddAsync(productResult.Value, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _cacheService.RemoveAsync(ProductsCacheKey, cancellationToken);
            return UnitResult.Success<Error>();
        }

        public async Task<Result<ProductDto, Error>> UpdateAsync(string id, UpdateProductDto productDto, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (!Guid.TryParse(id, out var productId))
                return Result.Failure<ProductDto, Error>(Errors.General.IncorrectGuidError());

            var product = await _productRepository.GetProductByIdAsync(productId, cancellationToken: cancellationToken);
            if (product == null)
                return Result.Failure<ProductDto, Error>(Errors.Product.ProductIsNullById());

            var subCategory = await _subCategoryRepository.GetSubCategoryByIdAsync(productDto.SubCategoryId, cancellationToken: cancellationToken);
            if (subCategory == null)
                return Result.Failure<ProductDto, Error>(Errors.SubCategory.SubCategoryIsNullById());

            var supplier = await _supplierRepository.GetSupplierByIdAsync(productDto.SupplierId, cancellationToken: cancellationToken);
            if (supplier == null)
                return Result.Failure<ProductDto, Error>(Errors.Supplier.SupplierIsNullById());

            var previousImagePath = product.ImagePath;

            string? imagePath = null;
            if (productDto.Image != null)
                imagePath = await _imageStorageService.SaveAsync(productDto.Image, ImagesSubfolder, cancellationToken);

            var updateResult = product.Update(
                productDto.Name,
                productDto.Description,
                productDto.Price,
                productDto.Sku,
                productDto.StockQuantity,
                productDto.SubCategoryId,
                productDto.SupplierId,
                productDto.IsActive,
                imagePath);

            if (updateResult.IsFailure)
                return Result.Failure<ProductDto, Error>(updateResult.Error);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _cacheService.RemoveAsync(ProductsCacheKey, cancellationToken);

            // Only drop the old file once the new one is safely persisted.
            if (imagePath != null)
                await _imageStorageService.DeleteAsync(previousImagePath, cancellationToken);

            // Built from `subCategory`/`supplier` (already fetched above) rather
            // than product.ToDto() - Update() nulls those nav properties when
            // the corresponding FK changes, so that path would throw here.
            return Result.Success<ProductDto, Error>(new ProductDto
            {
                Id = product.Id,
                Name = product.Name.Value,
                Description = product.Description.Value,
                Price = product.Price.Value,
                Sku = product.Sku.Value,
                StockQuantity = product.StockQuantity.Value,
                IsActive = product.IsActive,
                IsAvailable = product.IsAvailable,
                ImagePath = product.ImagePath,
                CreatedAtUtc = product.CreatedAtUtc,
                UpdatedUtc = product.UpdatedUtc,
                SubCategoryId = product.SubCategoryId,
                SubCategoryName = subCategory.Name.Value,
                SupplierId = product.SupplierId,
                SupplierName = supplier.Name.Value
            });
        }

        public async Task<UnitResult<Error>> DeleteAsync(string id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (!Guid.TryParse(id, out var productId))
                return UnitResult.Failure(Errors.General.IncorrectGuidError());

            var product = await _productRepository.GetProductByIdAsync(productId, cancellationToken: cancellationToken);
            if (product == null)
                return UnitResult.Failure(Errors.Product.ProductIsNullById());

            _productRepository.Delete(product);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _cacheService.RemoveAsync(ProductsCacheKey, cancellationToken);
            await _imageStorageService.DeleteAsync(product.ImagePath, cancellationToken);
            return UnitResult.Success<Error>();
        }
    }
}
