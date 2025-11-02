namespace library_api.Domain.Entities
{
    public class Book
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public int YearPublished { get; set; }
        public string? ISBN { get; set; }
        public bool IsAvailable { get; set; }
        public bool? IsBorrowed { get;set; }
        public int AvailableCopies { get; set; }
        public string? Language { get; set; }
        public required ICollection<Author> Authors { get; set; } = new List<Author>();
        public required ICollection<Genre> Genres { get; set; } = new List<Genre>();
        public ICollection<User>? Users { get; set; } = new List<User>();
    }
}
