namespace library_api.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public string? Role { get; set; } = "User"; // Default role is "User"
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true; // Default is active
        public bool IsLocked { get; set; } = false; // Default is not locked
        public bool IsDeleted { get; set; } = false; // Default is not deleted
        public ICollection<Book> Books { get; set; } = new List<Book>();

    }
}
