using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;

namespace library_api.Domain.Exceptions
{
    public class NotFoundException : DomainException
    {
        public NotFoundException(Guid bookId) : base("The requested resource was not found.", "NOT_FOUND", 404)
        {
            Details.Add("Resource", bookId);
        }
    }
}