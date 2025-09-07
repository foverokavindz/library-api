using library_api.Domain.Entities;
using library_api.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace library_api.Insfastructure.Repositories
{
    public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(DbContext dbContext) : base(dbContext) { }

        public async Task<IEnumerable<Author>> SearchAsync(string query)
        {
            return await _dbSet
                .Where(d => d.FirstName.Contains(query) || d.LastName.Contains(query) || (d.FirstName + d.LastName).Contains(query))
                .ToListAsync();
        }

        public async Task<IEnumerable<Author>>? GetByIdsAsync(List<Guid> guids)
        {
            var results = new List<Author>();

            foreach (var guid in guids)
            {
                var result = await _dbSet
                     .Where(a => a.Id == guid)
                     .FirstOrDefaultAsync();

                if (result != null)
                    results.Add(result);
            }

            return results;
        }
    }
}
