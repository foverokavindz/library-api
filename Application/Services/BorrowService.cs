using library_api.Application.DTO.BorrowBook;
using library_api.Application.Interfaces;
using library_api.Domain.Interfaces;

namespace library_api.Application.Services
{
    public class BorrowService : IBorrowService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IUserRepository _userRepository;

        public BorrowService(IBookRepository bookRepository, IUserRepository userRepository)
        {
            _bookRepository = bookRepository;
            _userRepository = userRepository;
        }

        public Task<BorrowBookResponseDto> BorrowBookAsync(BorrowBookRequestDto dto)
        {
            // validate the request

            // check whether book in available

            // check whether user is eligible to borrow

            // Check user borrowing limit

            // create borrow record

            // update book status to borrowed

            // return response dto
            throw new NotImplementedException();
        }
    }
}
