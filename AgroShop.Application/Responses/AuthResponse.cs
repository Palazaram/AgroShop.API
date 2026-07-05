namespace AgroShop.Application.Responses
{
    public class AuthResponse
    {
        public string AccessToken { get; init; } = default!;
        public string RefreshToken { get; init; } = default!;
    }
}
