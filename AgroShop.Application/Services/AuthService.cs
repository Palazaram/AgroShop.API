using AgroShop.Application.Dto.AuthDto;
using AgroShop.Application.Jwt;
using AgroShop.Application.Responses;
using AgroShop.Application.Interfaces;
using AgroShop.Core.Entities;
using AgroShop.Core.Shared;
using AgroShop.Core.ValueObjects;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AuthService> _logger;
        private static readonly PasswordHasher<User> _passwordHasher = new();

        public AuthService(
            IUserRepository userRepository,
            IJwtTokenHandler jwtTokenHandler,
            IUnitOfWork unitOfWork,
            IRoleRepository roleRepository,
            ILogger<AuthService> logger)
        {
            _userRepository = userRepository;
            _jwtTokenHandler = jwtTokenHandler;
            _unitOfWork = unitOfWork;
            _roleRepository = roleRepository;
            _logger = logger;
        }

        public async Task<Result<AuthResponse, Error>> RegisterAsync(CancellationToken cancellationToken, RegisterUserDto registerUserDto)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var userExistsByEmail = await _userRepository.UserExistsByEmailAsync(cancellationToken, registerUserDto.Email);
            if (userExistsByEmail)
                return Result.Failure<AuthResponse, Error>(Errors.Authentication.UserIsAlreadyExistsByEmail());

            var userExistsByPhone = await _userRepository.UserExistsByPhoneAsync(cancellationToken, registerUserDto.Phone);
            if (userExistsByPhone)
                return Result.Failure<AuthResponse, Error>(Errors.Authentication.UserIsAlreadyExistsByPhone());

            var lastNameResult = LastName.Create(registerUserDto.LastName).Value;
            var firstNameResult = FirstName.Create(registerUserDto.FirstName).Value;
            var patronymicResult = Patronymic.Create(registerUserDto.Patronymic).Value;
            var emailResult = Email.Create(registerUserDto.Email).Value;
            var phoneResult = Phone.Create(registerUserDto.Phone).Value;
            var user = User.Create(lastNameResult, firstNameResult, patronymicResult,
                emailResult, phoneResult, HashPassword(registerUserDto.Password), RoleConstants.CustomerId);

            await _userRepository.AddAsync(cancellationToken, user);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var role = await _roleRepository.GetRoleByIdAsync(cancellationToken, user.RoleId);
            user.AssignRole(role!);

            var authResponse = await _jwtTokenHandler.GenerateTokensAsync(user, cancellationToken);

            _logger.LogInformation("New user registered with id {UserId}", user.Id);

            return Result.Success<AuthResponse, Error>(authResponse);
        }

        public async Task<Result<AuthResponse, Error>> LoginAsync(CancellationToken cancellationToken, LoginUserDto loginUserDto)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var user = await _userRepository.GetUserByPhoneAsync(cancellationToken, loginUserDto.Phone);
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

        public async Task<UnitResult<Error>> LogOutAsync(CancellationToken cancellationToken, string? refreshToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            // The user may have already removed the cookie themselves; treat logout as successful.
            if (string.IsNullOrEmpty(refreshToken))
                return UnitResult.Success<Error>();

            await _jwtTokenHandler.RevokeRefreshTokenAsync(refreshToken, cancellationToken);

            return UnitResult.Success<Error>();
        }

        public async Task<Result<AuthResponse, Error>> RefreshTokensAsync(CancellationToken cancellationToken, string? refreshToken)
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
