using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace BaseServiceApiRest_Core.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (HttpRequestException ex)
            {
                await HandleErrorMessage(context, ex);
            }
            catch (Exception ex) // Captura outras exceções
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync("An unexpected error occurred.");
            }
        }

        private static async Task HandleErrorMessage(HttpContext context, HttpRequestException ex)
        {
            var statusCode = ex.StatusCode ?? HttpStatusCode.BadRequest;

            ProblemDetails problem = new()
            {
                Status = (int)statusCode,
                Type = statusCode.ToString(),
                Title = statusCode.ToString(),
                Detail = ex.Message
            };
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";

            var json = JsonSerializer.Serialize(problem);

            await context.Response.WriteAsync(json);
        }
    }
}
