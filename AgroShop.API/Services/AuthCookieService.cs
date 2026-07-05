using AgroShop.Application.Responses;

namespace AgroShop.API.Services
{
    /// <summary>
    /// Centralizes reading/writing the authentication cookies so their options
    /// (HttpOnly, Secure, SameSite, lifetime) are defined in a single place.
    /// </summary>
    public class AuthCookieService : IAuthCookieService
    {
        public const string AccessTokenCookie = "accessToken";
        public const string RefreshTokenCookie = "refreshToken";

        private readonly int _accessTokenExpirationMinutes;
        private readonly int _refreshTokenExpirationDays;

        public AuthCookieService(IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            _accessTokenExpirationMinutes = int.Parse(jwtSettings["AccessTokenExpirationMinutes"]!);
            _refreshTokenExpirationDays = int.Parse(jwtSettings["RefreshTokenExpirationDays"]!);
        }

        public void SetAuthCookies(HttpResponse response, AuthResponse tokens)
        {
            response.Cookies.Append(
                AccessTokenCookie,
                tokens.AccessToken,
                BuildOptions(DateTimeOffset.UtcNow.AddMinutes(_accessTokenExpirationMinutes)));

            response.Cookies.Append(
                RefreshTokenCookie,
                tokens.RefreshToken,
                BuildOptions(DateTimeOffset.UtcNow.AddDays(_refreshTokenExpirationDays)));
        }

        public void DeleteAuthCookies(HttpResponse response)
        {
            // Deletion only works when the options match the ones used when the cookie was set.
            response.Cookies.Delete(AccessTokenCookie, BuildOptions(null));
            response.Cookies.Delete(RefreshTokenCookie, BuildOptions(null));
        }

        private static CookieOptions BuildOptions(DateTimeOffset? expires) => new()
        {
            HttpOnly = true,
            Secure = true,
            // The SPA and the API are served from different origins, so the auth cookies
            // are sent on cross-site requests. That requires SameSite=None together with Secure.
            SameSite = SameSiteMode.None,
            Path = "/",
            Expires = expires
        };
    }
}
