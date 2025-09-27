using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace library_api.Domain.Exceptions
{
    public class BookNotFoundException : DomainException
    {
        public BookNotFoundException(Guid bookId)  : base($"Book with ID {bookId} not found", "BOOK_NOT_FOUND", 404)
        {
            Details.Add("BookId", bookId);
        }
    }
}