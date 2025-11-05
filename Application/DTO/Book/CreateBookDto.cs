using library_api.Application.DTO.Author;
using library_api.Application.DTO.Genre;

namespace library_api.Application.DTO.Book
{
    public class CreateBookDto
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int YearPublished { get; set; }
        public string? ISBN { get; set; }
        public string? Language { get; set; }

        // Use existing authors by ID
        public List<Guid> AuthorIds { get; set; } = [];
        public List<Guid> GenreIds { get; set; } = [];

        // You can take IDs instead of full objects
        public List<CreateAuthorDto> NewAuthors { get; set; } = [];
        public List<CreateGenreDto> NewGenres { get; set; } = [];
    }
}
