using System.Net;

namespace library_api.Application.Exceptions
{
    /// <summary>
    /// Exception thrown when access is forbidden
    /// </summary>
    public class ForbiddenException : BaseException
    {
        public override HttpStatusCode StatusCode => HttpStatusCode.Forbidden;
        public override string ErrorCode => "ACCESS_FORBIDDEN";

        public ForbiddenException(string message) : base(message)
        {
        }
    }
}
