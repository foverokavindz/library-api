using library_api.Application.DTO.Book;
using library_api.Domain.Entities;

namespace library_api.Application.Interfaces
{
    public interface IBookService : IBaseService<BookResponseDto, CreateBookDto, UpdateBookDto>
    {
        Task<IEnumerable<BookResponseDto>> GetAvailableAsync();
        Task<IEnumerable<BookResponseDto>> GetByAuthorAsync(Guid authorId);
        Task<IEnumerable<BookResponseDto>> GetByGenreAsync(Guid genreId);
        Task<IEnumerable<BookResponseDto>> SearchAsync(string query);
    }

}
