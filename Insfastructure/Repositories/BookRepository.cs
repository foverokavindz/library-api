using library_api.Domain.Entities;
using library_api.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace library_api.Insfastructure.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(DbContext dbContext) : base(dbContext) { }

        public async Task<IEnumerable<Book>> GetByAuthorAsync(Guid authorId)
        {
            return await _dbSet
                .Where(b => b.Authors.Any(a => a.Id == authorId))
                .ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetByGenreAsync(Guid genreId)
        {
            return await _dbSet
                .Where(b => b.Genres.Any(a => a.Id == genreId))
                .ToListAsync();
        }
        public async Task<IEnumerable<Book>>? GetAvailableAsync()
        {
            return await _dbSet
                .Where(b => b.IsAvailable)
                .ToListAsync();
        }
        public async Task<IEnumerable<Book>>? SearchAsync(string query)
        {
            return await _dbSet
                .Where(b => b.Title.Contains(query) || (b.Description != null && b.Description.Contains(query)))
                .ToListAsync();
        }
    }
}
