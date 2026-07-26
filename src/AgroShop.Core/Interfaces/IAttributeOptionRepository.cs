using AgroShop.Core.Entities;

namespace AgroShop.Core.Interfaces
{
    public interface IAttributeOptionRepository
    {
        Task<IEnumerable<AttributeOption>> GetAttributeOptionsAsync(bool asNoTracking = false, CancellationToken cancellationToken = default);
        Task<AttributeOption?> GetAttributeOptionByIdAsync(Guid id, bool asNoTracking = false, CancellationToken cancellationToken = default);
        Task AddAsync(AttributeOption attributeOption, CancellationToken cancellationToken);
        void Delete(AttributeOption attributeOption);
    }
}
