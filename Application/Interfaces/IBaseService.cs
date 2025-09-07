namespace library_api.Application.Interfaces
{
    public interface IBaseService<TDto, TCreateDto, TUpdateDto>
    {
        Task<TDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<TDto>> GetAllAsync();
        Task<TDto> CreateAsync(TCreateDto dto);
        Task UpdateAsync(Guid id, TUpdateDto dto);
        Task DeleteAsync(Guid id);
    }
}
