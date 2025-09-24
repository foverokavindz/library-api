using System.Net;

namespace library_api.Application.Exceptions
{
    /// <summary>
    /// Base exception class for application-specific exceptions
    /// </summary>
    public abstract class BaseException : Exception
    {
        /// <summary>
        /// HTTP status code to be returned
        /// </summary>
        public abstract HttpStatusCode StatusCode { get; }

        /// <summary>
        /// Error code for client identification
        /// </summary>
        public abstract string ErrorCode { get; }

        protected BaseException(string message) : base(message) { }

        protected BaseException(string message, Exception innerException) : base(message, innerException) { }
    }
}
