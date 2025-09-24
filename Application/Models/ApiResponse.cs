namespace library_api.Application.Models
{
    /// <summary>
    /// Standardized API response wrapper
    /// </summary>
    /// <typeparam name="T">Type of data being returned</typeparam>
    public class ApiResponse<T>
    {
        /// <summary>
        /// Indicates if the request was successful
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Response data (if successful)
        /// </summary>
        public T? Data { get; set; }

        /// <summary>
        /// Error information (if unsuccessful)
        /// </summary>
        public ErrorResponse? Error { get; set; }

        /// <summary>
        /// Additional metadata
        /// </summary>
        public Dictionary<string, object>? Metadata { get; set; }

        /// <summary>
        /// Create a successful response
        /// </summary>
        public static ApiResponse<T> SuccessResult(T data, Dictionary<string, object>? metadata = null)
        {
            return new ApiResponse<T>
            {
                Success = true,
                Data = data,
                Metadata = metadata
            };
        }

        /// <summary>
        /// Create an error response
        /// </summary>
        public static ApiResponse<T> ErrorResult(ErrorResponse error)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Error = error
            };
        }
    }
}
