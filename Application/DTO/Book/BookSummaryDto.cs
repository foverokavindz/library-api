namespace library_api.Application.DTO.Book
{

    //Lightweight DTO for lists
    public class BookSummaryDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int YearPublished { get; set; }
        public bool IsAvailable { get; set; }
        public int AvailableCopies { get; set; }
        public string AuthorNames { get; set; } = string.Empty;
        public string GenreNames { get; set; } = string.Empty;

    }
}
