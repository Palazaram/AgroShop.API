using AgroShop.Core.Entities;
using AgroShop.Core.Interfaces;
using AgroShop.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace AgroShop.Persistence.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly AgroShopDbContext _context;

        public RefreshTokenRepository(AgroShopDbContext context)
        {
            _context = context;
        }

        public async Task<RefreshToken?> GetRefreshTokenByHashAsync(CancellationToken cancellationToken, string tokenHash)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _context.RefreshTokens.SingleOrDefaultAsync(rt => rt.TokenHash == tokenHash, cancellationToken);
        }
    }
}
