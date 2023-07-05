using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OfferApp.Core.Exceptions;
using System.Net;

namespace OfferApp.Infrastructure.Exceptions
{
    internal class ErrorHandlerMiddleware : IMiddleware
    {
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(ILogger<ErrorHandlerMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                await HandleException(context, exception);
            }
        }

        private async Task HandleException(HttpContext httpContext, Exception exception)
        {
            _logger.LogError(exception, exception.Message);
            var error = GetError(exception);
            httpContext.Response.StatusCode = error.StatusCode;
            await httpContext.Response.WriteAsJsonAsync(new Dictionary<string, string>() { { "Error", error.Message } });
        }

        private record Error(int StatusCode, string Message);

        private static Error GetError(Exception exception)
            => exception switch
            {
                OfferException ex => new Error((int)HttpStatusCode.BadRequest, ex.Message),
                _ => new Error((int)HttpStatusCode.InternalServerError, "There was an error")
            };
    }
}
