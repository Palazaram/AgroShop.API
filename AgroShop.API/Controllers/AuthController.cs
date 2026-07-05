using AgroShop.API.Extensions;
using AgroShop.API.Responses;
using AgroShop.API.Services;
using AgroShop.Application.Dto.AuthDto;
using AgroShop.Application.Interfaces;
using FluentValidation;
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
        private readonly IValidator<LoginUserDto> _loginUserDtoValidator;
        private readonly IValidator<RegisterUserDto> _registerUserDtoValidator;

        public AuthController(
            IAuthService authService,
            IUserService userService,
            IAuthCookieService authCookieService,
            IValidator<LoginUserDto> loginUserDtoValidator,
            IValidator<RegisterUserDto> registerUserDtoValidator)
        {
            _authService = authService;
            _userService = userService;
            _authCookieService = authCookieService;
            _loginUserDtoValidator = loginUserDtoValidator;
            _registerUserDtoValidator = registerUserDtoValidator;
        }

        [AllowAnonymous]
        [HttpPost("registration")]
        public async Task<IActionResult> Registration([FromBody] RegisterUserDto registerUserDto, CancellationToken cancellationToken)
        {
            var validationResult = await _registerUserDtoValidator.ValidateAsync(registerUserDto, cancellationToken);

            if (!validationResult.IsValid)
                return FromValidation(validationResult);

            var result = await _authService.RegisterAsync(cancellationToken, registerUserDto);

            if (result.IsFailure)
                return result.Error.ToResponse();

            _authCookieService.SetAuthCookies(Response, result.Value);
            return Ok(Envelope.Ok());
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto loginUserDto, CancellationToken cancellationToken)
        {
            var validationResult = await _loginUserDtoValidator.ValidateAsync(loginUserDto, cancellationToken);

            if (!validationResult.IsValid)
                return FromValidation(validationResult);

            var result = await _authService.LoginAsync(cancellationToken, loginUserDto);

            if (result.IsFailure)
                return result.Error.ToResponse();

            _authCookieService.SetAuthCookies(Response, result.Value);
            return Ok(Envelope.Ok());
        }

        [AllowAnonymous]
        [HttpPost("logout")]
        public async Task<IActionResult> LogOut(CancellationToken cancellationToken)
        {
            var refreshToken = Request.Cookies[AuthCookieService.RefreshTokenCookie];
            var result = await _authService.LogOutAsync(cancellationToken, refreshToken);

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
            var result = await _authService.RefreshTokensAsync(cancellationToken, refreshToken);

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
