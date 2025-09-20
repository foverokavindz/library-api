using library_api.Application.DTO.BorrowBook;

namespace library_api.Application.Interfaces
{
    public interface IBorrowService
    {
        Task<BorrowBookResponseDto> BorrowBookAsync(BorrowBookRequestDto dto);
    }
}
