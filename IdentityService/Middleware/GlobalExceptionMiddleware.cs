using IdentityService.Common;
using IdentityService.Exceptions;
using System.Text.Json;

namespace IdentityService.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        ILogger<GlobalExceptionMiddleware> _logger;
        public GlobalExceptionMiddleware(RequestDelegate request , ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = request; 
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context , Exception exception) 
        { 
            context.Response.ContentType = "application/json";

            int statusCode = exception switch
            {
                ConflictException => StatusCodes.Status409Conflict,
                UnauthorizedException => StatusCodes.Status401Unauthorized,
                NotFoundException => StatusCodes.Status404NotFound,

                _ => StatusCodes.Status500InternalServerError
            };

            context.Response.StatusCode = statusCode;

            //var response = new
            //{
            //    StatusCode = statusCode,
            //    Message = exception.Message
            //};
            var response = ApiResponse<object>.FailureResponse(
                   exception.Message
               );

            var json = JsonSerializer.Serialize(response);
            
            await context.Response.WriteAsync(json);


         }
        }
}

