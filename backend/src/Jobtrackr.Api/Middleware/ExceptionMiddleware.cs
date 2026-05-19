using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

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
            context.Response.StatusCode =
                StatusCodes.Status500InternalServerError;

            context.Response.ContentType =
                "application/json";

            var problem = new ProblemDetails
            {
                Title = "Internal Server Error",
                Status = StatusCodes.Status500InternalServerError,
                Detail = "An unexpected error occurred"
            };

            await context.Response.WriteAsJsonAsync(
                problem);
        }
    }
}