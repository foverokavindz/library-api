namespace library_api.Application.DTO.BorrowBook
{
    public class BorrowBookRequestDto
    {
        public required Guid UserId { get; set; }
        public required Guid BookId { get; set; }
    }
}
