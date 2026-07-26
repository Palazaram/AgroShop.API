using AgroShop.Core.Entities;

namespace AgroShop.Core.Interfaces
{
    public interface IProductAttributeRepository
    {
        Task<IEnumerable<ProductAttribute>> GetProductAttributesAsync(bool asNoTracking = false, CancellationToken cancellationToken = default);
        Task<ProductAttribute?> GetProductAttributeByIdAsync(Guid id, bool asNoTracking = false, CancellationToken cancellationToken = default);
        Task AddAsync(ProductAttribute productAttribute, CancellationToken cancellationToken);
        void Delete(ProductAttribute productAttribute);
    }
}
