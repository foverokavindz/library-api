using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace library_api.Domain.Exceptions
{
    public class InsufficientCopiesException : DomainException
    {
        public InsufficientCopiesException(Guid bookId) : base("Not enough copies available", "INSUFFICIENT_COPIES", 400)
        {
            Details.Add("BookId", bookId);
        }
    }
}