namespace library_api.Application.DTO.Author
{
    public class AuthorDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty ;

        public string FullName => $"{FirstName} {LastName}";
    }
}
