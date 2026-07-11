using AgroShop.Application.Dto.AuthDto;
using AgroShop.Application.Responses;
using AgroShop.Core.Shared;
using CSharpFunctionalExtensions;

namespace AgroShop.Application.Interfaces
{
    public interface IAuthService
    {
        Task<Result<AuthResponse, Error>> RegisterAsync(RegisterUserDto registerUserDto, CancellationToken cancellationToken);
        Task<Result<AuthResponse, Error>> LoginAsync(LoginUserDto loginUserDto, CancellationToken cancellationToken);
        Task<UnitResult<Error>> LogOutAsync(string? refreshToken, CancellationToken cancellationToken);
        Task<Result<AuthResponse, Error>> RefreshTokensAsync(string? refreshToken, CancellationToken cancellationToken);
    }
}
