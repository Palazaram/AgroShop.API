using AgroShop.Application.Responses;
using AgroShop.Core.Entities;
using AgroShop.Core.Interfaces;
using AgroShop.Core.Shared;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AgroShop.Application.Jwt
{
    public class JwtTokenHandler : IJwtTokenHandler
    {
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IConfigurationSection _jwtSettings;
        private readonly IUnitOfWork _unitOfWork;

        private readonly int _accessTokenExpirationMinutes;
        private readonly int _refreshTokenExpirationDays;

        public JwtTokenHandler(
            IUserRepository userRepository, 
            IRefreshTokenRepository refreshTokenRepository, 
            IConfiguration configuration, 
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _jwtSettings = configuration.GetSection("JwtSettings");
            _unitOfWork = unitOfWork;

            _accessTokenExpirationMinutes = int.Parse(_jwtSettings["AccessTokenExpirationMinutes"]!);
            _refreshTokenExpirationDays = int.Parse(_jwtSettings["RefreshTokenExpirationDays"]!);
        }

        public async Task<AuthResponse> GenerateTokensAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var accessToken = GenerateAccessToken(user);

            var refreshTokenPlain = GenerateRefreshToken();
            var refreshTokenHash = HashToken(refreshTokenPlain);
            var refreshToken = RefreshToken.Create(user.Id, refreshTokenHash, DateTime.UtcNow.AddDays(_refreshTokenExpirationDays));

            user.AddRefreshToken(refreshToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new AuthResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshTokenPlain
            };
        }

        public async Task RevokeRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var tokenHash = HashToken(refreshToken);
            var token = await _refreshTokenRepository.GetRefreshTokenByHashAsync(cancellationToken, tokenHash);

            if (token != null)
            {
                token.Revoke();
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<Result<AuthResponse, Error>> RefreshTokensAsync(string refreshToken, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var tokenHash = HashToken(refreshToken);
            var token = await _refreshTokenRepository.GetRefreshTokenByHashAsync(cancellationToken, tokenHash);

            if (token == null || token.IsRevoked || token.ExpiryDate < DateTime.UtcNow)
                return Result.Failure<AuthResponse, Error>(Errors.Authentication.RefreshTokenIsInvalid());

            var user = await _userRepository.GetUserByIdAsync(cancellationToken, token.UserId);

            if (user == null)
                return Result.Failure<AuthResponse, Error>(Errors.User.UserIsNullById());

            token.Revoke();
            var response = await GenerateTokensAsync(user, cancellationToken);

            return response;
        }

        private string GenerateAccessToken(User user) 
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings["SecretKey"]!));
            var creds = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                //new Claim(ClaimTypes.Name, user.Login.Value),
                //new Claim(ClaimTypes.Email, user.Email.Value),
                new Claim(ClaimTypes.MobilePhone, user.Phone.Value),
                new Claim(ClaimTypes.Role, user.Role.Name)
            };

            var token = new JwtSecurityToken(
                issuer: _jwtSettings["Issuer"],
                audience: _jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_accessTokenExpirationMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        }

        private string HashToken(string token)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(token);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
