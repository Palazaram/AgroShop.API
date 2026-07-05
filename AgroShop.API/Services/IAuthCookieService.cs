using AgroShop.Application.Responses;

namespace AgroShop.API.Services
{
    public interface IAuthCookieService
    {
        void SetAuthCookies(HttpResponse response, AuthResponse tokens);
        void DeleteAuthCookies(HttpResponse response);
    }
}
