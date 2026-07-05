using AgroShop.Core.Entities;

namespace AgroShop.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> UserExistsByEmailAsync(string? email, CancellationToken cancellationToken);
        Task<bool> UserExistsByPhoneAsync(string phone, CancellationToken cancellationToken);
        Task<User?> GetUserByPhoneAsync(string phone, CancellationToken cancellationToken);
        Task<User?> GetUserByIdAsync(Guid id, CancellationToken cancellationToken);
        Task AddAsync(User user, CancellationToken cancellationToken);
    }
}
