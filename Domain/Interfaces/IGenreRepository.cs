using library_api.Domain.Entities;

namespace library_api.Domain.Interfaces
{
    public interface IGenreRepository : IGenericRepository<Genre>
    {
        Task<IEnumerable<Genre>>? GetByIdsAsync(List<Guid> guids);
    }
}
