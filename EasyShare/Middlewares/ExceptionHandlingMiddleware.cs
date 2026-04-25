using EasyShare.Application.Common.Exceptions;
using System.Text.Json;

namespace EasyShare.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
        catch (Exception ex)
        {
            _logger.LogError(ex, "Сталася помилка під час обробки запиту.");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var (statusCode, response) = exception switch
        {
            FluentValidation.ValidationException ve => (
                StatusCodes.Status400BadRequest,
                (object)new
                {
                    Title = "Помилка валідації",
                    Status = StatusCodes.Status400BadRequest,
                    Detail = "Одне або кілька полів не пройшли перевірку.",
                    Errors = ve.Errors
                        .GroupBy(e => e.PropertyName)
                        .ToDictionary(
                            g => g.Key,
                            g => g.Select(e => e.ErrorMessage).ToArray()
                        )
                }
            ),

            NotFoundException nfe => (
                StatusCodes.Status404NotFound,
                (object)new
                {
                    Title = "Не знайдено",
                    Status = StatusCodes.Status404NotFound,
                    Detail = nfe.Message
                }
            ),

            ConflictException ce => (
                StatusCodes.Status409Conflict,
                (object)new
                {
                    Title = "Конфлікт даних",
                    Status = StatusCodes.Status409Conflict,
                    Detail = ce.Message
                }
            ),

            UnauthorizedException ue => (
                StatusCodes.Status401Unauthorized,
                (object)new
                {
                    Title = "Помилка авторизації",
                    Status = StatusCodes.Status401Unauthorized,
                    Detail = ue.Message
                }
            ),

            BadRequestException bre => (
                StatusCodes.Status400BadRequest,
                (object)new
                {
                    Title = "Некоректний запит",
                    Status = StatusCodes.Status400BadRequest,
                    Detail = bre.Message
                }
            ),

            _ => (
                StatusCodes.Status500InternalServerError,
                (object)new
                {
                    Title = "Помилка сервера",
                    Status = StatusCodes.Status500InternalServerError,
                    Detail = "Виникла непередбачувана помилка. Ми вже працюємо над цим."
                }
            )
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        var result = JsonSerializer.Serialize(response, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        await context.Response.WriteAsync(result);
    }
}
