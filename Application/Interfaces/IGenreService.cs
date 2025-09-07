using library_api.Application.DTO.Author;
using library_api.Application.DTO.Genre;

namespace library_api.Application.Interfaces
{
    public interface IGenreService : IBaseService<GenreDto, CreateGenreDto, UpdateGenreDto>
    {
        Task<IEnumerable<GenreDto>> GetByIdsAsync(List<Guid> guids);
    }
}
