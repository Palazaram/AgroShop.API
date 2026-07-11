using AgroShop.API.Responses;
using System.Net;
using System.Text.Json;

namespace AgroShop.API.Middlewares
{
    public sealed class ExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<ExceptionHandler> _logger;

        public ExceptionHandler(RequestDelegate next, IWebHostEnvironment env, ILogger<ExceptionHandler> logger)
        {
            _next = next;
            _env = env;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }

        private Task HandleException(HttpContext context, Exception exception)
        {
            _logger.LogError(exception, "Unhandled exception while processing {Method} {Path}",
                context.Request.Method, context.Request.Path);

            string errorMessage = _env.IsProduction() ? "Internal server error" : "Exception: " + exception.Message;

            var error = new ResponseError(
                ErrorCode: "internal.server.error",
                ErrorMessage: errorMessage,
                null
            );

            var envelope = Envelope.Error([error]);
            string result = JsonSerializer.Serialize(envelope);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return context.Response.WriteAsync(result);
        }
    }
}
