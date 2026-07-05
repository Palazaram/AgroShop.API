using AgroShop.Core.Entities;

namespace AgroShop.Core.Interfaces
{
    public interface IUserRepository
    {
        //Task<bool> UserExistsByLoginAsync(CancellationToken cancellationToken, string login);
        Task<bool> UserExistsByEmailAsync(CancellationToken cancellationToken, string? email);
        Task<bool> UserExistsByPhoneAsync(CancellationToken cancellationToken, string phone);
        //Task<User?> GetUserByLoginAsync(CancellationToken cancellationToken, string login);
        Task<User?> GetUserByPhoneAsync(CancellationToken cancellationToken, string phone);
        Task<User?> GetUserByIdAsync(CancellationToken cancellationToken, Guid Id);
        Task AddAsync(CancellationToken cancellationToken, User user);
    }
}
