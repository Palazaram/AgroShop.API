using AgroShop.Application.Dto.AuthDto;
using AgroShop.Application.Jwt;
using AgroShop.Application.Responses;
using AgroShop.Application.Interfaces;
using AgroShop.Core.Entities;
using AgroShop.Core.Shared;
using AgroShop.Core.Interfaces;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace AgroShop.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IJwtTokenHandler _jwtTokenHandler;
        private readonly ILogger<AuthService> _logger;
        private static readonly PasswordHasher<User> _passwordHasher = new();

        public AuthService(
            IUserRepository userRepository,
            IJwtTokenHandler jwtTokenHandler,
            IRoleRepository roleRepository,
            ILogger<AuthService> logger)
        {
            _userRepository = userRepository;
            _jwtTokenHandler = jwtTokenHandler;
            _roleRepository = roleRepository;
            _logger = logger;
        }

        public async Task<Result<AuthResponse, Error>> RegisterAsync(RegisterUserDto registerUserDto, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var userExistsByEmail = await _userRepository.UserExistsByEmailAsync(registerUserDto.Email, cancellationToken);
            if (userExistsByEmail)
                return Result.Failure<AuthResponse, Error>(Errors.Authentication.UserIsAlreadyExistsByEmail());

            var userExistsByPhone = await _userRepository.UserExistsByPhoneAsync(registerUserDto.Phone, cancellationToken);
            if (userExistsByPhone)
                return Result.Failure<AuthResponse, Error>(Errors.Authentication.UserIsAlreadyExistsByPhone());

            var userResult = User.Create(
                registerUserDto.LastName, registerUserDto.FirstName, registerUserDto.Patronymic,
                registerUserDto.Email, registerUserDto.Phone, HashPassword(registerUserDto.Password),
                RoleConstants.CustomerId);

            if (userResult.IsFailure)
                return Result.Failure<AuthResponse, Error>(userResult.Error);

            var user = userResult.Value;
            await _userRepository.AddAsync(user, cancellationToken);

            var role = await _roleRepository.GetRoleByIdAsync(user.RoleId, cancellationToken);
            user.AssignRole(role!);

            // The new user and their refresh token are persisted together in a single
            // SaveChanges inside GenerateTokensAsync, so registration is one atomic transaction.
            var authResponse = await _jwtTokenHandler.GenerateTokensAsync(user, cancellationToken);

            _logger.LogInformation("New user registered with id {UserId}", user.Id);

            return Result.Success<AuthResponse, Error>(authResponse);
        }

        public async Task<Result<AuthResponse, Error>> LoginAsync(LoginUserDto loginUserDto, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var user = await _userRepository.GetUserByPhoneAsync(loginUserDto.Phone, cancellationToken);
            if (user is null)
            {
                _logger.LogWarning("Failed login attempt: no user found for the provided phone number");
                return Result.Failure<AuthResponse, Error>(Errors.Authentication.IncorrectPhone());
            }

            if (!VerifyPassword(loginUserDto.Password, user.PasswordHash))
            {
                _logger.LogWarning("Failed login attempt for user {UserId}: incorrect password", user.Id);
                return Result.Failure<AuthResponse, Error>(Errors.Authentication.IncorrectPassword());
            }

            var authResponse = await _jwtTokenHandler.GenerateTokensAsync(user, cancellationToken);
            return Result.Success<AuthResponse, Error>(authResponse);
        }

        public async Task<UnitResult<Error>> LogOutAsync(string? refreshToken, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            // The user may have already removed the cookie themselves; treat logout as successful.
            if (string.IsNullOrEmpty(refreshToken))
                return UnitResult.Success<Error>();

            await _jwtTokenHandler.RevokeRefreshTokenAsync(refreshToken, cancellationToken);

            return UnitResult.Success<Error>();
        }

        public async Task<Result<AuthResponse, Error>> RefreshTokensAsync(string? refreshToken, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (string.IsNullOrEmpty(refreshToken))
                return Result.Failure<AuthResponse, Error>(Errors.Authentication.RefreshTokenIsNull());

            return await _jwtTokenHandler.RefreshTokensAsync(refreshToken, cancellationToken);
        }

        private static string HashPassword(string password) => _passwordHasher.HashPassword(null!, password);

        private static bool VerifyPassword(string password, string hash) =>
            _passwordHasher.VerifyHashedPassword(null!, hash, password) == PasswordVerificationResult.Success;
    }
}
