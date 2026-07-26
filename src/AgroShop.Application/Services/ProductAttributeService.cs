using AgroShop.Application.Dto.ProductAttributeDto;
using AgroShop.Application.Interfaces;
using AgroShop.Application.Mappers;
using AgroShop.Core.Entities;
using AgroShop.Core.Interfaces;
using AgroShop.Core.Shared;
using CSharpFunctionalExtensions;

namespace AgroShop.Application.Services
{
    public class ProductAttributeService : IProductAttributeService
    {
        // Backstop only - Add/Delete below invalidate this explicitly.
        private const string ProductAttributesCacheKey = "product-attributes:all";
        private static readonly TimeSpan ProductAttributesCacheDuration = TimeSpan.FromMinutes(15);

        private readonly IProductAttributeRepository _productAttributeRepository;
        private readonly ISubCategoryRepository _subCategoryRepository;
        private readonly IAttributeRepository _attributeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICacheService _cacheService;

        public ProductAttributeService(
            IProductAttributeRepository productAttributeRepository,
            ISubCategoryRepository subCategoryRepository,
            IAttributeRepository attributeRepository,
            IUnitOfWork unitOfWork,
            ICacheService cacheService)
        {
            _productAttributeRepository = productAttributeRepository;
            _subCategoryRepository = subCategoryRepository;
            _attributeRepository = attributeRepository;
            _unitOfWork = unitOfWork;
            _cacheService = cacheService;
        }

        public async Task<Result<IEnumerable<ProductAttributeDto>, Error>> GetProductAttributesAsync(bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var cached = await _cacheService.GetAsync<List<ProductAttributeDto>>(ProductAttributesCacheKey, cancellationToken);
            if (cached != null)
                return Result.Success<IEnumerable<ProductAttributeDto>, Error>(cached);

            var productAttributes = await _productAttributeRepository.GetProductAttributesAsync(asNoTracking, cancellationToken);
            var productAttributeDtos = productAttributes.ToDto()
                .OrderBy(pa => pa.SubCategoryName)
                .ThenBy(pa => pa.AttributeName)
                .ToList();

            await _cacheService.SetAsync(ProductAttributesCacheKey, productAttributeDtos, ProductAttributesCacheDuration, cancellationToken);

            return Result.Success<IEnumerable<ProductAttributeDto>, Error>(productAttributeDtos);
        }

        public async Task<Result<ProductAttributeDto, Error>> GetProductAttributeByIdAsync(string id, bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (!Guid.TryParse(id, out var productAttributeId))
                return Result.Failure<ProductAttributeDto, Error>(Errors.General.IncorrectGuidError());

            var productAttribute = await _productAttributeRepository.GetProductAttributeByIdAsync(productAttributeId, asNoTracking, cancellationToken);

            if (productAttribute == null)
                return Result.Failure<ProductAttributeDto, Error>(Errors.ProductAttribute.ProductAttributeIsNullById());

            return Result.Success<ProductAttributeDto, Error>(productAttribute.ToDto());
        }

        public async Task<UnitResult<Error>> AddAsync(AddProductAttributeDto productAttributeDto, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var subCategory = await _subCategoryRepository.GetSubCategoryByIdAsync(productAttributeDto.SubCategoryId, cancellationToken: cancellationToken);
            if (subCategory == null)
                return UnitResult.Failure(Errors.SubCategory.SubCategoryIsNullById());

            var attribute = await _attributeRepository.GetAttributeByIdAsync(productAttributeDto.AttributeId, cancellationToken: cancellationToken);
            if (attribute == null)
                return UnitResult.Failure(Errors.Attribute.AttributeIsNullById());

            var productAttributeResult = ProductAttribute.Create(productAttributeDto.SubCategoryId, productAttributeDto.AttributeId);
            if (productAttributeResult.IsFailure)
                return UnitResult.Failure(productAttributeResult.Error);

            await _productAttributeRepository.AddAsync(productAttributeResult.Value, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _cacheService.RemoveAsync(ProductAttributesCacheKey, cancellationToken);
            return UnitResult.Success<Error>();
        }

        public async Task<UnitResult<Error>> DeleteAsync(string id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (!Guid.TryParse(id, out var productAttributeId))
                return UnitResult.Failure(Errors.General.IncorrectGuidError());

            var productAttribute = await _productAttributeRepository.GetProductAttributeByIdAsync(productAttributeId, cancellationToken: cancellationToken);
            if (productAttribute == null)
                return UnitResult.Failure(Errors.ProductAttribute.ProductAttributeIsNullById());

            _productAttributeRepository.Delete(productAttribute);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _cacheService.RemoveAsync(ProductAttributesCacheKey, cancellationToken);
            return UnitResult.Success<Error>();
        }
    }
}
