using library_api.Application.DTO.Author;
using library_api.Application.DTO.Genre;

namespace library_api.Application.Interfaces
{
    public interface IGenreService : IBaseService<GenreResponseDto, CreateGenreDto, UpdateGenreDto>
    {
        Task<IEnumerable<GenreResponseDto>> GetByIdsAsync(List<Guid> guids);
    }
}
