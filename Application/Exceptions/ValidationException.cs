using System.Net;

namespace library_api.Application.Exceptions
{
    /// <summary>
    /// Exception thrown when validation fails
    /// </summary>
    public class ValidationException : BaseException
    {
        public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
        public override string ErrorCode => "VALIDATION_ERROR";

        public Dictionary<string, string[]> Errors { get; }

        public ValidationException(string message) : base(message)
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(Dictionary<string, string[]> errors) 
            : base("One or more validation errors occurred.")
        {
            Errors = errors;
        }
    }
}
