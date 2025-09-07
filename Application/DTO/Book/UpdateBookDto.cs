namespace library_api.Application.DTO.Book
{
    public class UpdateBookDto
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int YearPublished { get; set; }
        public string? ISBN { get; set; }
        public string? Language { get; set; }

        public List<Guid> AuthorIds { get; set; } = new();
        public List<Guid> GenreIds { get; set; } = new();
    }
}
