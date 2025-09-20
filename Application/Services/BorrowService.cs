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
            
        }
    }
}
