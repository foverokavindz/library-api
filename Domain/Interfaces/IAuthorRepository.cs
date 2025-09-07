using library_api.Domain.Entities;

namespace library_api.Domain.Interfaces
{
    public interface IAuthorRepository : IGenericRepository<Author>
    {
        Task<IEnumerable<Author>>? SearchAsync(string query);
        Task<IEnumerable<Author>>? GetByIdsAsync(List<Guid> guids);
    }
}
