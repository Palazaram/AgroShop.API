using AgroShop.Core.Entities;

namespace AgroShop.Core.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken?> GetRefreshTokenByHashAsync(CancellationToken cancellationToken, string tokenHash);
    }
}
