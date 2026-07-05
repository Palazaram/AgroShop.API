using AgroShop.Application.Dto.AuthDto;
using AgroShop.Application.Responses;
using AgroShop.Core.Shared;
using CSharpFunctionalExtensions;

namespace AgroShop.Application.Interfaces
{
    public interface IAuthService
    {
        Task<Result<AuthResponse, Error>> RegisterAsync(CancellationToken cancellationToken, RegisterUserDto registerUserDto);
        Task<Result<AuthResponse, Error>> LoginAsync(CancellationToken cancellationToken, LoginUserDto loginUserDto);
        Task<Result<object?, Error>> LogOutAsync(CancellationToken cancellationToken, string? refreshToken);
        Task<Result<AuthResponse, Error>> RefreshTokensAsync(CancellationToken cancellationToken, string? refreshToken);
    }
}
