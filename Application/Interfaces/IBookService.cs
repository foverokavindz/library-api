using library_api.Application.DTO.Book;
using library_api.Domain.Entities;

namespace library_api.Application.Interfaces
{
    public interface IBookService : IBaseService<BookDto, CreateBookDto, UpdateBookDto>
    {
        Task<IEnumerable<BookDto>> GetAvailableAsync();
        Task<IEnumerable<BookDto>> GetByAuthorAsync(Guid authorId);
        Task<IEnumerable<BookDto>> GetByGenreAsync(Guid genreId);
        Task<IEnumerable<BookDto>> SearchAsync(string query);
    }

}
