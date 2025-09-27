using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace library_api.Domain.Exceptions
{
    public abstract class DomainException : Exception
    {
        public string ErrorCode { get; }
        public int StatusCode { get; }
        public Dictionary<string, object> Details { get; }

        protected DomainException(string message, string errorCode, int statusCode = 400) : base(message)
        {
            ErrorCode = errorCode;
            StatusCode = statusCode;
            Details = new Dictionary<string, object>();
        }
    }
}