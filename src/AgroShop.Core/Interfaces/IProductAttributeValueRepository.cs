using AgroShop.Core.Entities;

namespace AgroShop.Core.Interfaces
{
    // Thinner than the usual repository shape - ProductAttributeValue has no
    // standalone service/controller (values are set through Product's own
    // Add/Update), so the only real query pattern needed is "give me this
    // product's values."
    public interface IProductAttributeValueRepository
    {
        Task<IEnumerable<ProductAttributeValue>> GetByProductIdAsync(Guid productId, bool asNoTracking = false, CancellationToken cancellationToken = default);
        Task AddAsync(ProductAttributeValue value, CancellationToken cancellationToken);
        void Delete(ProductAttributeValue value);
    }
}
