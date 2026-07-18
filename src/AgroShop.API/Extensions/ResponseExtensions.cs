using AgroShop.API.Responses;
using AgroShop.Core.Enums;
using AgroShop.Core.Shared;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AgroShop.API.Extensions
{
    public static class ResponseExtensions
    {
        public static ActionResult ToResponse(this Error error) 
        {
            var statusCode = error.Type switch 
            {
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.InternalServerError => StatusCodes.Status500InternalServerError,
                ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
                _ => StatusCodes.Status500InternalServerError
            };

            var responseError = new ResponseError(error.Code, error.Message, null);

            var envelop = Envelope.Error([responseError]);

            return new ObjectResult(envelop) 
            {
                StatusCode = statusCode
            };
        }

        public static ActionResult ToValidationErrorResponse(this ValidationResult result)
        {
            if (result.IsValid) 
            {
                throw new InvalidOperationException("Результат не може бути успішним.");
            }

            var responseErrors = from validationError in result.Errors
                                 let error = Error.Deserialize(validationError.ErrorMessage)
                                 select new ResponseError(error.Code, error.Message, validationError.PropertyName);

            var envelope = Envelope.Error(responseErrors);

            return new ObjectResult(envelope)
            {
                StatusCode = StatusCodes.Status400BadRequest,
            };
        }

        // Model-binding failures (malformed JSON, missing "required" DTO members, wrong
        // property types) are caught by [ApiController]'s automatic validation before
        // ValidationFilter or the action ever runs, so they need their own conversion:
        // ModelState messages are plain framework text, not a serialized Error, so they
        // can't go through Error.Deserialize like FluentValidation's ToValidationErrorResponse.
        public static ActionResult ToValidationErrorResponse(this ModelStateDictionary modelState)
        {
            var responseErrors = from entry in modelState
                                  where entry.Value is { Errors.Count: > 0 }
                                  from modelError in entry.Value!.Errors
                                  select new ResponseError(
                                      "invalid.request",
                                      string.IsNullOrEmpty(modelError.ErrorMessage) ? "Некоректне значення" : modelError.ErrorMessage,
                                      entry.Key);

            var envelope = Envelope.Error(responseErrors);

            return new ObjectResult(envelope)
            {
                StatusCode = StatusCodes.Status400BadRequest,
            };
        }
    }
}
