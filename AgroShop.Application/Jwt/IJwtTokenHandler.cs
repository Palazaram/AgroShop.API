using AgroShop.Application.Responses;
using AgroShop.Core.Entities;
using AgroShop.Core.Shared;
using CSharpFunctionalExtensions;

namespace AgroShop.Application.Jwt
{
    public interface IJwtTokenHandler
    {
        Task<AuthResponse> GenerateTokensAsync(User user, CancellationToken cancellationToken);
        Task RevokeRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken);
        Task<Result<AuthResponse, Error>> RefreshTokensAsync(string refreshToken, CancellationToken cancellationToken);
    }
}
