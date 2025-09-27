using library_api.Domain.Exceptions;

namespace library_api.Application.Models
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
        public ApiError? Error { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string RequestId { get; set; } = Guid.NewGuid().ToString();
        public Dictionary<string, object>? Metadata { get; set; }

        // Success factory methods
        public static ApiResponse<T> SuccessResponse(T data, Dictionary<string, object>? metadata = null) => new()
        {
            Success = true,
            Data = data,
            Metadata = metadata
        };

        public static ApiResponse<object> SuccessResponse() => new()
        {
            Success = true,
            Data = new { }
        };

        // Error factory methods
        public static ApiResponse<T> ErrorResponse(string message, string code, int statusCode = 400, List<ValidationError>? validationErrors = null, Dictionary<string, object>? details = null) => new()
        {
            Success = false,
            Error = new ApiError
            {
                Message = message,
                Code = code,
                StatusCode = statusCode,
                ValidationErrors = validationErrors,
                Details = details
            }
        };

        public static ApiResponse<T> ErrorResponse(DomainException ex) => new()
        {
            Success = false,
            Error = new ApiError
            {
                Message = ex.Message,
                Code = ex.ErrorCode,
                StatusCode = ex.StatusCode,
                Details = ex.Details
            }
        };
    }




}
