using AgroShop.Application.Dto.ProductDto;
using AgroShop.Application.QueryParameters;
using AgroShop.Application.Responses;
using AgroShop.Core.Entities;
using AgroShop.Core.Shared;
using AgroShop.Core.ValueObjects;
using System.Linq.Expressions;
using CSharpFunctionalExtensions;

namespace AgroShop.Application.Interfaces
{
    public interface IProductService
    {
        Task<Result<PaginatedResult<ClientProductDto>, Error>> GetProductsForClientAsync(ProductQueryParameters query, CancellationToken cancellationToken);
        Task<Result<ClientProductDto?, Error>> GetClientProductByIdAsync(Guid id, CancellationToken cancellationToken, bool asNoTracking);
        
        Task<Result<IEnumerable<TDto>, Error>> GetProductsDTOAsync<TDto>(Expression<Func<Product, TDto>> selector, CancellationToken cancellationToken, Func<IQueryable<Product>, IQueryable<Product>>? filter = null);
        
        Task<Result<Product, Error>> AddProductAsync(CreateProductDto createProductDTO, CancellationToken cancellationToken);
        Task<Result> UpdateProductAsync(EditProductDto editProductDTO, CancellationToken cancellationToken);
        Task<Result> DeleteProductAsync(Guid productId, CancellationToken cancellationToken);
    }
}
