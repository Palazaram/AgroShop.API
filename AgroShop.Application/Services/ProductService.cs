using AgroShop.Application.Dto.ProductDto;
using AgroShop.Application.Extensions;
using AgroShop.Application.Interfaces;
using AgroShop.Application.QueryParameters;
using AgroShop.Application.Responses;
using AgroShop.Core.Entities;
using AgroShop.Core.Interfaces;
using AgroShop.Core.Shared;
using AgroShop.Core.ValueObjects;
using CSharpFunctionalExtensions;
using FluentValidation;
using System.Linq.Expressions;

namespace AgroShop.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductImageService _productImageService;

        private readonly IValidator<CreateProductDto> _createValidator;
        private readonly IValidator<EditProductDto> _editValidator;

        public ProductService(
            IProductRepository productRepository,
            IProductImageService productImageService,
            IValidator<CreateProductDto> createValidator,
            IValidator<EditProductDto> editValidator)
        {
            _productRepository = productRepository;
            _productImageService = productImageService;

            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        public Task<Result<Product, Error>> AddProductAsync(CreateProductDto createProductDTO, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Result> DeleteProductAsync(Guid productId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Result<ClientProductDto?, Error>> GetClientProductByIdAsync(Guid id, CancellationToken cancellationToken, bool asNoTracking)
        {
            throw new NotImplementedException();
        }

        public Task<Result<IEnumerable<TDto>, Error>> GetProductsDTOAsync<TDto>(Expression<Func<Product, TDto>> selector, CancellationToken cancellationToken, Func<IQueryable<Product>, IQueryable<Product>>? filter = null)
        {
            throw new NotImplementedException();
        }

        public Task<Result<PaginatedResult<ClientProductDto>, Error>> GetProductsForClientAsync(ProductQueryParameters query, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Result> UpdateProductAsync(EditProductDto editProductDTO, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        //// Хз пока что с ним делать, ставить паблик или приват. 
        //// По идеи чистый Продукт мне не нужен, использую ДТО 
        //private async Task<Result<IEnumerable<Product>>> GetProductsAsync(CancellationToken cancellationToken, bool asNoTracking = false, Func<IQueryable<Product>, IQueryable<Product>>? filter = null)
        //{
        //    cancellationToken.ThrowIfCancellationRequested();

        //    var products = await _productRepository.GetProductsAsync(cancellationToken, asNoTracking, filter);

        //    return Result<IEnumerable<Product>>.Success(products);
        //}

        //public async Task<Result<PaginatedResult<ClientProductDto>>> GetProductsForClientAsync(ProductQueryParameters query, CancellationToken cancellationToken)
        //{
        //    cancellationToken.ThrowIfCancellationRequested();

        //    var productsResult = await GetProductsAsync
        //        (
        //            cancellationToken,
        //            asNoTracking: true,
        //            filter: q =>
        //            {
        //                if (!string.IsNullOrWhiteSpace(query.Search))
        //                    q = q.Where(p => p.Name.Contains(query.Search));

        //                if (query.CategoryId.HasValue)
        //                    q = q.Where(p => p.SubCategory.CategoryId == new CategoryId(query.CategoryId.Value));

        //                if (query.SupplierId.HasValue)
        //                    q = q.Where(p => p.SupplierId == new SupplierId(query.SupplierId.Value));

        //                if (query.SubCategoryId.HasValue)
        //                    q = q.Where(p => p.SubCategoryId == new SubCategoryId(query.SubCategoryId.Value));

        //                q = q.OrderBy(p => p.Name);

        //                return q;
        //            }
        //        );

        //    if (!productsResult.IsSuccess)
        //        return Result<PaginatedResult<ClientProductDto>>.Failure(productsResult.Error);

        //    var filtered = productsResult.Data;
        //    var totalCount = filtered.Count();
        //    var pagedItems = filtered
        //        .Skip((query.Page - 1) * query.PageSize)
        //        .Take(query.PageSize)
        //        .Select(p => p.ToClientDto());

        //    var result = new PaginatedResult<ClientProductDto>
        //    {
        //        Items = pagedItems,
        //        TotalCount = totalCount
        //    };

        //    return Result<PaginatedResult<ClientProductDto>>.Success(result);
        //}

        //public async Task<Result<IEnumerable<TDto>>> GetProductsDTOAsync<TDto>(Expression<Func<Product, TDto>> selector, CancellationToken cancellationToken, Func<IQueryable<Product>, IQueryable<Product>>? filter = null)
        //{
        //    cancellationToken.ThrowIfCancellationRequested();
        //    var dtoProducts = await _productRepository.GetProductsDTOAsync(selector, cancellationToken, filter);
        //    return Result<IEnumerable<TDto>>.Success(dtoProducts);
        //}

        //// Тут тоже самое, что и с GetProductsAsync
        //private async Task<Result<Product?>> GetProductByIdAsync(ProductId id, CancellationToken cancellationToken, bool asNoTracking = true)
        //{
        //    cancellationToken.ThrowIfCancellationRequested();
        //    var product = await _productRepository.GetProductByIdAsync(id, cancellationToken, asNoTracking);
        //    return Result<Product?>.Success(product);
        //}

        //public async Task<Result<ClientProductDto?>> GetClientProductByIdAsync(ProductId id, CancellationToken cancellationToken, bool asNoTracking)
        //{
        //    var productResult = await GetProductByIdAsync(id, cancellationToken, asNoTracking);

        //    if (!productResult.IsSuccess)
        //        return Result<ClientProductDto?>.Failure(productResult.Error);

        //    var dto = productResult.Data?.ToClientDto();
        //    return Result<ClientProductDto?>.Success(dto);
        //}

        //public async Task<Result<Product>> AddProductAsync(CreateProductDto createProductDTO, CancellationToken cancellationToken)
        //{
        //    cancellationToken.ThrowIfCancellationRequested();

        //    var validationResult = await _createValidator.ValidateAsync(createProductDTO, cancellationToken);
        //    if (!validationResult.IsValid)
        //    {
        //        return Result<Product>.Failure(validationResult.Errors.ToValidationError());
        //    }

        //    var imagePathResult = await _productImageService.SaveImageAsync(cancellationToken, createProductDTO.Image!, new SubCategoryId(createProductDTO.SubCategoryId!.Value), new SupplierId(createProductDTO.SupplierId!.Value), createProductDTO.Name!);

        //    if (!imagePathResult.IsSuccess)
        //    {
        //        return Result<Product>.Failure(imagePathResult.Error);
        //    }

        //    var product = Product.Create(
        //        createProductDTO.Name!,
        //        createProductDTO.Description,
        //        createProductDTO.Price!.Value,
        //        new SubCategoryId(createProductDTO.SubCategoryId!.Value),
        //        new SupplierId(createProductDTO.SupplierId!.Value),
        //        imagePathResult.Data
        //    );

        //    await _productRepository.AddProductAsync(product, cancellationToken);

        //    return Result<Product>.Success(product);
        //}

        //public async Task<Result> UpdateProductAsync(EditProductDto editProductDTO, CancellationToken cancellationToken)
        //{
        //    cancellationToken.ThrowIfCancellationRequested();

        //    var validationResult = await _editValidator.ValidateAsync(editProductDTO, cancellationToken);
        //    if (!validationResult.IsValid)
        //    {
        //        return Result<Product>.Failure(validationResult.Errors.ToValidationError());
        //    }

        //    var existingProduct = await GetProductByIdAsync(new ProductId(editProductDTO.Id), cancellationToken, asNoTracking: false);

        //    if (existingProduct.Data == null)
        //        return Result.Failure(ProductErrors.NotFound(new ProductId(editProductDTO.Id)));

        //    Result<string>? newImagePathResult = null;

        //    if (editProductDTO.Image != null)
        //    {
        //        _productImageService.DeleteImage(editProductDTO.ImagePath);
        //        newImagePathResult = await _productImageService.SaveImageAsync(cancellationToken, editProductDTO.Image, new SubCategoryId(editProductDTO.SubCategoryId!.Value), new SupplierId(editProductDTO.SupplierId!.Value), editProductDTO.Name!);
        //    }
        //    else if ((existingProduct.Data.SupplierId != editProductDTO.SupplierId || existingProduct.Data.SubCategoryId != editProductDTO.SubCategoryId) && !string.IsNullOrEmpty(existingProduct.Data.ImagePath))
        //    {
        //        newImagePathResult = await _productImageService.MoveImageAsync(cancellationToken, existingProduct.Data.ImagePath, new SubCategoryId(editProductDTO.SubCategoryId!.Value), new SupplierId(editProductDTO.SupplierId!.Value), editProductDTO.Name!);
        //    }

        //    if (newImagePathResult != null && !newImagePathResult.IsSuccess)
        //    {
        //        return Result.Failure(newImagePathResult.Error);
        //    }

        //    existingProduct.Data.Update(
        //        editProductDTO.Name!,
        //        editProductDTO.Description,
        //        editProductDTO.Price!.Value,
        //        new SubCategoryId(editProductDTO.SubCategoryId!.Value),
        //        new SupplierId(editProductDTO.SupplierId!.Value),
        //        editProductDTO.IsAvailable,
        //        newImagePathResult?.Data // если null — старый путь останется
        //    );

        //    await _productRepository.UpdateProductAsync(existingProduct.Data, cancellationToken);
        //    return Result.Success();
        //}

        //public async Task<Result> DeleteProductAsync(Guid productId, CancellationToken cancellationToken)
        //{
        //    cancellationToken.ThrowIfCancellationRequested();

        //    var product = await _productRepository.GetProductByIdAsync(new ProductId(productId), cancellationToken);
        //    if (product == null)
        //        return Result.Failure(ProductErrors.NotFound(new ProductId(productId)));

        //    _productImageService.DeleteImage(product.ImagePath);

        //    await _productRepository.DeleteProductAsync(product, cancellationToken);
        //    return Result.Success();
        //}
    }
}
