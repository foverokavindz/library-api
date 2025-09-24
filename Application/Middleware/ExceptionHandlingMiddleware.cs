using library_api.Application.Exceptions;
using library_api.Application.Models;
using System.Net;
using System.Text.Json;

namespace library_api.Application.Middleware
{
    /// <summary>
    /// Global exception handling middleware
    /// </summary>
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
            catch (Exception exception)
            {
                _logger.LogError(exception, "An unhandled exception occurred. TraceId: {TraceId}", 
                    context.TraceIdentifier);

                await HandleExceptionAsync(context, exception);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            var errorResponse = exception switch
            {
                BaseException baseEx => new ErrorResponse
                {
                    ErrorCode = baseEx.ErrorCode,
                    Message = baseEx.Message,
                    Details = exception is ValidationException validationEx ? validationEx.Errors : null,
                    TraceId = context.TraceIdentifier
                },
                ArgumentException => new ErrorResponse
                {
                    ErrorCode = "INVALID_ARGUMENT",
                    Message = "Invalid argument provided.",
                    Details = exception.Message,
                    TraceId = context.TraceIdentifier
                },
                ArgumentNullException => new ErrorResponse
                {
                    ErrorCode = "NULL_ARGUMENT",
                    Message = "Required argument is null.",
                    Details = exception.Message,
                    TraceId = context.TraceIdentifier
                },
                InvalidOperationException => new ErrorResponse
                {
                    ErrorCode = "INVALID_OPERATION",
                    Message = "Invalid operation attempted.",
                    Details = exception.Message,
                    TraceId = context.TraceIdentifier
                },
                UnauthorizedAccessException => new ErrorResponse
                {
                    ErrorCode = "UNAUTHORIZED_ACCESS",
                    Message = "Unauthorized access attempted.",
                    TraceId = context.TraceIdentifier
                },
                TimeoutException => new ErrorResponse
                {
                    ErrorCode = "OPERATION_TIMEOUT",
                    Message = "Operation timed out.",
                    TraceId = context.TraceIdentifier
                },
                _ => new ErrorResponse
                {
                    ErrorCode = "INTERNAL_SERVER_ERROR",
                    Message = "An internal server error occurred.",
                    TraceId = context.TraceIdentifier
                }
            };

            response.StatusCode = exception switch
            {
                BaseException baseEx => (int)baseEx.StatusCode,
                ArgumentException => (int)HttpStatusCode.BadRequest,
                ArgumentNullException => (int)HttpStatusCode.BadRequest,
                InvalidOperationException => (int)HttpStatusCode.BadRequest,
                UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
                TimeoutException => (int)HttpStatusCode.RequestTimeout,
                _ => (int)HttpStatusCode.InternalServerError
            };

            var apiResponse = ApiResponse<object>.ErrorResult(errorResponse);
            var jsonResponse = JsonSerializer.Serialize(apiResponse, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            await response.WriteAsync(jsonResponse);
        }
    }
}
