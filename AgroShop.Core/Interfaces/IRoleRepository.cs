using AgroShop.Core.Entities;

namespace AgroShop.Core.Interfaces
{
    public interface IRoleRepository
    {
        Task<Role?> GetRoleByIdAsync(CancellationToken cancellationToken, Guid Id);
    }
}
