using System.Net;

namespace library_api.Application.Exceptions
{
    /// <summary>
    /// Exception thrown when a conflict occurs (e.g., duplicate resource)
    /// </summary>
    public class ConflictException : BaseException
    {
        public override HttpStatusCode StatusCode => HttpStatusCode.Conflict;
        public override string ErrorCode => "RESOURCE_CONFLICT";

        public ConflictException(string message) : base(message)
        {
        }
    }
}
