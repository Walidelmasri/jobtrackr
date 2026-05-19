using System.Text.Json;

namespace Jobtrackr.Api.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(
        RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(
        HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception)
        {
            context.Response.StatusCode = 500;

            context.Response.ContentType =
                "application/json";

            var response = new
            {
                title = "Internal Server Error",
                status = 500,
                message = "An unexpected error occurred"
            };

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(response));
        }
    }
}