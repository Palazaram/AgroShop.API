using AgroShop.Core.Entities;
using Attribute = AgroShop.Core.Entities.Attribute;

namespace AgroShop.Core.Interfaces
{
    public interface IAttributeRepository
    {
        Task<IEnumerable<Attribute>> GetAttributesAsync(bool asNoTracking = false, CancellationToken cancellationToken = default);
        Task<Attribute?> GetAttributeByIdAsync(Guid id, bool asNoTracking = false, CancellationToken cancellationToken = default);
        Task AddAsync(Attribute attribute, CancellationToken cancellationToken);
        void Delete(Attribute attribute);
    }
}
