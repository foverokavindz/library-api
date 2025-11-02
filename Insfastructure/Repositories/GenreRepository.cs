using library_api.Domain.Entities;
using library_api.Domain.Interfaces;
using library_api.Insfastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace library_api.Insfastructure.Repositories
{
    public class GenreRepository : GenericRepository<Genre>, IGenreRepository
    {
        public GenreRepository(AppDbContext dbContext) : base(dbContext) { }

        public async Task<IEnumerable<Genre>>? GetByIdsAsync(List<Guid> guids)
        {
            var results = new List<Genre>();

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
