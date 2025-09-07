namespace library_api.Domain.Entities
{
    public class Genre
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public ICollection<Book>? Books { get; set; } = new List<Book>();
    }
}