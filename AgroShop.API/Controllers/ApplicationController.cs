using AgroShop.API.Extensions;
using AgroShop.API.Responses;
using AgroShop.Core.Shared;
using CSharpFunctionalExtensions;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace AgroShop.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApplicationController : ControllerBase
    {
        protected IActionResult FromResult<T>(Result<T, Error> result)
        {
            if (result.IsSuccess)
            {
                return Ok(Envelope.Ok(result.Value));
            }

            return result.Error.ToResponse();
        }

        protected IActionResult FromValidation(ValidationResult validationResult)
        {
            return validationResult.ToValidationErrorResponse();
        }
    }
}
