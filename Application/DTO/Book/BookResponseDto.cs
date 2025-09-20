using library_api.Application.DTO.Author;
using library_api.Application.DTO.Genre;

namespace library_api.Application.DTO.Book
{
    public class BookResponseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int YearPublished { get; set; }
        public string? ISBN { get; set; }
        public bool IsAvailable { get; set; }
        public bool? IsBorrowed { get; set; }
        public int AvailableCopies { get; set; }
        public string? Language { get; set; }
        public ICollection<AuthorResponseDto> Authors { get; set; } = new List<AuthorResponseDto>();
        public ICollection<GenreResponseDto> Genres { get; set; } = new List<GenreResponseDto>();

    }
}
