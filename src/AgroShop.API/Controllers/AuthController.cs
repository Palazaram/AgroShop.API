using AgroShop.API.Extensions;
using AgroShop.API.Responses;
using AgroShop.API.Services;
using AgroShop.Application.Dto.AuthDto;
using AgroShop.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AgroShop.API.Controllers
{
    public class AuthController : ApplicationController
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly IAuthCookieService _authCookieService;

        public AuthController(
            IAuthService authService,
            IUserService userService,
            IAuthCookieService authCookieService)
        {
            _authService = authService;
            _userService = userService;
            _authCookieService = authCookieService;
        }

        [AllowAnonymous]
        [HttpPost("registration")]
        public async Task<IActionResult> Registration([FromBody] RegisterUserDto registerUserDto, CancellationToken cancellationToken)
        {
            var result = await _authService.RegisterAsync(registerUserDto, cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();

            _authCookieService.SetAuthCookies(Response, result.Value);
            return Ok(Envelope.Ok());
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto loginUserDto, CancellationToken cancellationToken)
        {
            var result = await _authService.LoginAsync(loginUserDto, cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();

            _authCookieService.SetAuthCookies(Response, result.Value);
            return Ok(Envelope.Ok());
        }

        [AllowAnonymous]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout(CancellationToken cancellationToken)
        {
            var refreshToken = Request.Cookies[AuthCookieService.RefreshTokenCookie];
            var result = await _authService.LogOutAsync(refreshToken, cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();

            _authCookieService.DeleteAuthCookies(Response);
            return Ok(Envelope.Ok());
        }

        [AllowAnonymous]
        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshTokens(CancellationToken cancellationToken)
        {
            var refreshToken = Request.Cookies[AuthCookieService.RefreshTokenCookie];
            var result = await _authService.RefreshTokensAsync(refreshToken, cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();

            _authCookieService.SetAuthCookies(Response, result.Value);
            return Ok(Envelope.Ok());
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> Me(CancellationToken cancellationToken)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _userService.GetUserByIdAsync(userId, cancellationToken);
            return FromResult(result);
        }
    }
}
