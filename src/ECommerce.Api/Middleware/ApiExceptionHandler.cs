using ECommerce.Domain.Exceptions;

namespace ECommerce.Api.Middleware;

public class ApiExceptionHandler(RequestDelegate next, ILogger<ApiExceptionHandler> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error no controlado: {Message}", ex.Message);
            await WriteResponseAsync(context, ex);
        }
    }

    private static async Task WriteResponseAsync(HttpContext context, Exception ex)
    {
        var (statusCode, message) = ex switch
        {
            ResourceNotFoundException => (StatusCodes.Status404NotFound, ex.Message),
            AppException => (StatusCodes.Status422UnprocessableEntity, ex.Message),
            UnauthorizedAccessException => (StatusCodes.Status401Unauthorized, "No autorizado."),
            _ => (StatusCodes.Status500InternalServerError, "Ocurrió un error inesperado en el servidor.")
        };

        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsJsonAsync(new { status = statusCode, message });
    }
}
