using AgroShop.Application.Responses;
using AgroShop.Core.Shared;
using CSharpFunctionalExtensions;

namespace AgroShop.Application.Interfaces
{
    public interface IUserService
    {
        Task<Result<UserResponse, Error>> GetUserByIdAsync(string? rawUserId, CancellationToken cancellationToken);
    }
}
