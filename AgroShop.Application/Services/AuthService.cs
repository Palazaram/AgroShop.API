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

namespace AgroShop.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IJwtTokenHandler _jwtTokenHandler;
        private readonly IUnitOfWork _unitOfWork;
        private static readonly PasswordHasher<User> _passwordHasher = new();

        public AuthService(
            IUserRepository userRepository, 
            IJwtTokenHandler jwtTokenHandler, 
            IUnitOfWork unitOfWork, 
            IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _jwtTokenHandler = jwtTokenHandler;
            _unitOfWork = unitOfWork;
            _roleRepository = roleRepository;
        }

        public async Task<Result<AuthResponse, Error>> RegisterAsync(CancellationToken cancellationToken, RegisterUserDto registerUserDto)
        {
            cancellationToken.ThrowIfCancellationRequested();

            //var userExistsByLogin = await _userRepository.UserExistsByLoginAsync(cancellationToken, registerUserDto.Login);
            //if (userExistsByLogin)
            //    return Result.Failure<object?, Error>(Errors.User.UserIsAlreadyExistsByLogin());

            var userExistsByEmail = await _userRepository.UserExistsByEmailAsync(cancellationToken, registerUserDto.Email);
            if (userExistsByEmail)
                return Result.Failure<AuthResponse, Error>(Errors.Authentication.UserIsAlreadyExistsByEmail());

            var userExistsByPhone = await _userRepository.UserExistsByPhoneAsync(cancellationToken, registerUserDto.Phone);
            if (userExistsByPhone)
                return Result.Failure<AuthResponse, Error>(Errors.Authentication.UserIsAlreadyExistsByPhone());

            //var loginResult = Login.Create(registerUserDto.Login).Value;
            var lastNameResult = LastName.Create(registerUserDto.LastName).Value;
            var firstNameResult = FirstName.Create(registerUserDto.FirstName).Value;
            var patronymicResult = Patronymic.Create(registerUserDto.Patronymic).Value;
            var emailResult = Email.Create(registerUserDto.Email).Value;
            var phoneResult = Phone.Create(registerUserDto.Phone).Value;
            var user = User.Create(/*loginResult,*/ lastNameResult, firstNameResult, patronymicResult, 
                emailResult, phoneResult, HashPassword(registerUserDto.Password), RoleConstants.CustomerId);

            await _userRepository.AddAsync(cancellationToken, user);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var role = await _roleRepository.GetRoleByIdAsync(cancellationToken, user.RoleId);
            user.AssignRole(role!);

            var authResponse = await _jwtTokenHandler.GenerateTokensAsync(user, cancellationToken);

            return Result.Success<AuthResponse, Error>(authResponse);
        }

        public async Task<Result<AuthResponse, Error>> LoginAsync(CancellationToken cancellationToken, LoginUserDto loginUserDto)
        {
            cancellationToken.ThrowIfCancellationRequested();

            //var user = await _userRepository.GetUserByLoginAsync(cancellationToken, loginUserDto.Login);
            var user = await _userRepository.GetUserByPhoneAsync(cancellationToken, loginUserDto.Phone);
            if (user is null)
                //return Result.Failure<AuthResponse, Error>(Errors.User.IncorrectLoginOrPassword());
                return Result.Failure<AuthResponse, Error>(Errors.Authentication.IncorrectPhone());

            if (!VerifyPassword(loginUserDto.Password, user.PasswordHash))
                return Result.Failure<AuthResponse, Error>(Errors.Authentication.IncorrectPassword());

            var authResponse = await _jwtTokenHandler.GenerateTokensAsync(user, cancellationToken);
            return Result.Success<AuthResponse, Error>(authResponse);
        }

        public async Task<Result<object?, Error>> LogOutAsync(CancellationToken cancellationToken, string? refreshToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (string.IsNullOrEmpty(refreshToken))
                return Result.Success<object?, Error>(null); // Допустим пользователь удалил сам токен из куки

            await _jwtTokenHandler.RevokeRefreshTokenAsync(refreshToken, cancellationToken);

            return Result.Success<object?, Error>(null);
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
