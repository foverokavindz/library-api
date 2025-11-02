 namespace library_api.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(Guid id);
        Task<IEnumerable<T?>> GetAllAsync(); // this will return only read  // https://www.notion.so/c-29f9179f0cf480a48321fb1201537295?source=copy_link
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(Guid id);
        
        IQueryable<T> AsQueryable();

    }
}
