namespace library_api.Domain.Entities
{
    public class Author
    {
        public Guid Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public ICollection<Book>? Books { get; set; } = new List<Book>();
    }
}