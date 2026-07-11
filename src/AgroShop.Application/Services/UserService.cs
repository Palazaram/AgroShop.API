using AgroShop.Application.Interfaces;
using AgroShop.Application.Responses;
using AgroShop.Core.Interfaces;
using AgroShop.Core.Shared;
using CSharpFunctionalExtensions;

namespace AgroShop.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<UserResponse, Error>> GetUserByIdAsync(string? rawUserId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (string.IsNullOrEmpty(rawUserId))
                return Result.Failure<UserResponse, Error>(Errors.Authentication.Unauthorized());

            if(!Guid.TryParse(rawUserId, out var userGuidId))
                return Result.Failure<UserResponse, Error>(Errors.General.IncorrectGuidError());

            var user = await _userRepository.GetUserByIdAsync(userGuidId, cancellationToken);

            if (user is null)
                return Result.Failure<UserResponse, Error>(Errors.Authentication.Unauthorized());

            var response = new UserResponse
            {
                Id = user.Id,
                Phone = user.Phone.Value,
                LastName = user.LastName.Value,
                FirstName = user.FirstName.Value,
                Patronymic = user.Patronymic.Value,
                Email = user.Email.Value,
                Role = user.Role.Name
            };

            return Result.Success<UserResponse, Error>(response);
        }
    }
}
