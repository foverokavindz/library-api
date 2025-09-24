using System.Net;

namespace library_api.Application.Exceptions
{
    /// <summary>
    /// Exception thrown when a business rule is violated
    /// </summary>
    public class BusinessRuleViolationException : BaseException
    {
        public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
        public override string ErrorCode => "BUSINESS_RULE_VIOLATION";

        public BusinessRuleViolationException(string message) : base(message)
        {
        }
    }
}
