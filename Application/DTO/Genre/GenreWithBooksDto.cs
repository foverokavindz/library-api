namespace library_api.Application.DTO.Genre
{
    public class GenreWithBooksDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<BookResponseDto>? Books { get; set; } = new List<BookResponseDto>();

    }
}
