namespace library_api.Application.DTO.BorrowBook
{
    public class BorrowBookResponseDto
    {
        public required Guid UserId { get; set; }
        public required Guid BookId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }
    }
}
