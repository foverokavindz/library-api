namespace library_api.Application.DTO.Book
{
    public class BookDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int YearPublished { get; set; }
        public bool IsAvailable { get; set; }
        public int AvailableCopies { get; set; }
        public string? Language { get; set; }

        // Example: flattening related data
        public IEnumerable<string> Authors { get; set; } = new List<string>();
        public IEnumerable<string> Genres { get; set; } = new List<string>();
    }
}
