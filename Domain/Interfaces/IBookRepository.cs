using library_api.Domain.Entities;

namespace library_api.Domain.Interfaces
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        Task<IEnumerable<Book>> GetByAuthorAsync(Guid authorId);
        Task<IEnumerable<Book>>? GetByGenreAsync(Guid genreId);
        Task<IEnumerable<Book>>? GetAvailableAsync();
        Task<IEnumerable<Book>>? SearchAsync(string query);

    }
}
