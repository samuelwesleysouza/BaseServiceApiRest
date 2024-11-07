using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;

namespace BaseServiceApiRest_Core.Middleware;

public class ExceptionHandlerMiddleware : IMiddleware
{
    private ILogger<ExceptionHandlerMiddleware> _logger;
    public ExceptionHandlerMiddleware(ILoggerFactory logger)
    {
        _logger = logger.CreateLogger<ExceptionHandlerMiddleware>();
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (HttpRequestException ex)
        {
            await HandleErrorMessage(context, ex);
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