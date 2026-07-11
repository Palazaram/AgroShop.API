using AgroShop.API.Extensions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AgroShop.API.Filters
{
    /// <summary>
    /// Runs the matching FluentValidation validator for every action argument that has one,
    /// so controllers no longer have to validate manually. On failure it short-circuits the
    /// request with the standard validation-error envelope.
    /// </summary>
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            foreach (var argument in context.ActionArguments.Values)
            {
                if (argument is null)
                    continue;

                var validatorType = typeof(IValidator<>).MakeGenericType(argument.GetType());

                if (context.HttpContext.RequestServices.GetService(validatorType) is not IValidator validator)
                    continue;

                var validationResult = await validator.ValidateAsync(
                    new ValidationContext<object>(argument), context.HttpContext.RequestAborted);

                if (!validationResult.IsValid)
                {
                    context.Result = validationResult.ToValidationErrorResponse();
                    return;
                }
            }

            await next();
        }
    }
}
