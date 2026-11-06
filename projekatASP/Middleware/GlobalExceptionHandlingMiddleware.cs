using Application.Exceptions;
using FluentValidation;

namespace projekatASP.API.Middleware
{
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";

                if (ex is ValidationException validationException)
                {
                    context.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;

                    var errors = validationException.Errors.Select(x => new
                    {
                        error = x.ErrorMessage,
                        property = x.PropertyName
                    });

                    await context.Response.WriteAsJsonAsync(errors);

                    return;
                }

                if (ex is BadRequestException)
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                }

                else if (ex is ConflictException)
                {
                    context.Response.StatusCode = StatusCodes.Status409Conflict;
                }

                else if (ex is NotFoundException)
                {
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                }

                else if (ex is UnauthorizedException)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                }

                else if(ex is ForbidenException)
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                }

                else if (ex is InternalServerException)
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                }

                await context.Response.WriteAsJsonAsync(new
                {
                    message = ex.Message
                });

                return;
            }
        }
    }
}
