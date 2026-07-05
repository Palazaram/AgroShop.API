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

        //public async Task<bool> UserExistsByLoginAsync(CancellationToken cancellationToken, string login)
        //{
        //    cancellationToken.ThrowIfCancellationRequested();
        //    return await _context.Users.AnyAsync(u => u.Login.Value == login);
        //}

        public async Task<bool> UserExistsByEmailAsync(CancellationToken cancellationToken, string? email)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (email == null)
                return false;

            return await _context.Users.AnyAsync(u => u.Email != null && u.Email.Value == email.ToLowerInvariant(), cancellationToken);
        }

        public async Task<bool> UserExistsByPhoneAsync(CancellationToken cancellationToken, string phone)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _context.Users.AnyAsync(u => u.Phone.Value == phone, cancellationToken);
        }

        //public async Task<User?> GetUserByLoginAsync(CancellationToken cancellationToken, string login)
        //{
        //    cancellationToken.ThrowIfCancellationRequested();
        //    return await _context.Users.IncludeAll().SingleOrDefaultAsync(u => u.Login.Value == login);
        //}

        public async Task<User?> GetUserByPhoneAsync(CancellationToken cancellationToken, string phone)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _context.Users.IncludeAll().SingleOrDefaultAsync(u => u.Phone.Value == phone, cancellationToken);
        }

        public async Task<User?> GetUserByIdAsync(CancellationToken cancellationToken, Guid Id)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _context.Users.IncludeAll().SingleOrDefaultAsync(u => u.Id == Id, cancellationToken);
        }

        public async Task AddAsync(CancellationToken cancellationToken, User user)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await _context.Users.AddAsync(user, cancellationToken);
        }
    }
}
