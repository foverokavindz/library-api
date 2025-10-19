using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using library_api.Application.Models;
using library_api.Domain.Exceptions;

namespace library_api.Application.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
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
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var response = exception switch
            {
                DomainException domainEx => HandleDomainException(domainEx),
                UnauthorizedAccessException => HandleUnauthorizedException(),
                TimeoutException timeoutEx => HandleTimeoutException(timeoutEx),
                HttpRequestException httpEx => HandleHttpException(httpEx),

                // default case for unhandled exceptions
                _ => HandleGenericException(exception)
            };

            context.Response.StatusCode = response.Error!.StatusCode; // TODO - ASK
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));       // TODO - ASK     

        }

        private ApiResponse<object> HandleDomainException(DomainException ex)
        {
            return ApiResponse<object>.ErrorResponse(ex); // This will handle all the exceptions which inherited from DomainException
        }

        private ApiResponse<object> HandleUnauthorizedException()
        {
            return ApiResponse<object>.ErrorResponse("Access denied", "UNAUTHORIZED", 401);
        }

        private ApiResponse<object> HandleTimeoutException(TimeoutException ex)
        {
            return ApiResponse<object>.ErrorResponse("Request timeout", "REQUEST_TIMEOUT", 408);
        }

        private ApiResponse<object> HandleHttpException(HttpRequestException ex)
        {
            return ApiResponse<object>.ErrorResponse("External service unavailable", "SERVICE_UNAVAILABLE", 503);
        }

        private ApiResponse<object> HandleGenericException(Exception ex)
        {
            return ApiResponse<object>.ErrorResponse("An unexpected error occurred", "INTERNAL_ERROR", 500);
        }


    }
}