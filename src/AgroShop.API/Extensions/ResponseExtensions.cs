using AgroShop.API.Responses;
using AgroShop.Core.Enums;
using AgroShop.Core.Shared;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

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
    }
}
