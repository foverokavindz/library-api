namespace library_api.Application.Models
{
    /// <summary>
    /// Standardized error response model
    /// </summary>
    public class ErrorResponse
    {
        /// <summary>
        /// Error code for client identification
        /// </summary>
        public string ErrorCode { get; set; } = string.Empty;

        /// <summary>
        /// Human-readable error message
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Additional error details (optional)
        /// </summary>
        public object? Details { get; set; }

        /// <summary>
        /// Timestamp when the error occurred
        /// </summary>
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Unique identifier for tracking this error
        /// </summary>
        public string TraceId { get; set; } = string.Empty;
    }
}
