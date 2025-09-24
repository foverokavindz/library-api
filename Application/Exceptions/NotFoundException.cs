using System.Net;

namespace library_api.Application.Exceptions
{
    /// <summary>
    /// Exception thrown when a requested resource is not found
    /// </summary>
    public class NotFoundException : BaseException
    {
        public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;
        public override string ErrorCode => "RESOURCE_NOT_FOUND";

        public NotFoundException(string resourceName, object key) 
            : base($"{resourceName} with identifier '{key}' was not found.")
        {
        }

        public NotFoundException(string message) : base(message)
        {
        }
    }
}
