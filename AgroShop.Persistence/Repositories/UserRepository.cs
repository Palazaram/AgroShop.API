using AgroShop.Core.Entities;
using AgroShop.Core.Interfaces;
using AgroShop.Persistence.Data;
using AgroShop.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace AgroShop.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AgroShopDbContext _context;

        public UserRepository(AgroShopDbContext context)
        {
            _context = context;
        }

        public async Task<bool> UserExistsByEmailAsync(string? email, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (email == null)
                return false;

            return await _context.Users.AnyAsync(u => u.Email.Value == email.ToLowerInvariant(), cancellationToken);
        }

        public async Task<bool> UserExistsByPhoneAsync(string phone, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _context.Users.AnyAsync(u => u.Phone.Value == phone, cancellationToken);
        }

        public async Task<User?> GetUserByPhoneAsync(string phone, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _context.Users.IncludeAll().SingleOrDefaultAsync(u => u.Phone.Value == phone, cancellationToken);
        }

        public async Task<User?> GetUserByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _context.Users.IncludeAll().SingleOrDefaultAsync(u => u.Id == id, cancellationToken);
        }

        public async Task AddAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await _context.Users.AddAsync(user, cancellationToken);
        }
    }
}
