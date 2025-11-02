using library_api.Application.DTO.Author;
using library_api.Application.DTO.Genre;
using library_api.Application.Interfaces;
using library_api.Domain.Entities;
using library_api.Domain.Interfaces;

namespace library_api.Application.Services
{
    // TODO: Books are not handled here
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _repository;

        public GenreService(IGenreRepository repository)
        {
            _repository = repository;
        }

        public async Task<GenreResponseDto?> GetByIdAsync(Guid id)
        {
            var genre = await _repository.GetByIdAsync(id);
            return genre == null ? null : MaptoDto(genre);
        }

        public async Task<IEnumerable<GenreResponseDto>> GetAllAsync()
        {
            var genres = await _repository.GetAllAsync();
            return genres.Where(g => g != null).Select(g => MaptoDto(g!)); // learn this
        }

        public async Task<GenreResponseDto> CreateAsync(CreateGenreDto dto)
        {
            var genre = new Genre
            {
                Id = Guid.NewGuid(),
                Name = dto.Name
            };
            await _repository.AddAsync(genre);
            return MaptoDto(genre);
        }

        public async Task UpdateAsync(Guid id, UpdateGenreDto dto)
        {
            var genre = await _repository.GetByIdAsync(id);
            if (genre == null)
            {
                throw new KeyNotFoundException("Genre not found");
            }
            genre.Name = dto.Name;
            await _repository.UpdateAsync(genre);
        }

        public async Task DeleteAsync(Guid id)
        {
            var genre = await _repository.GetByIdAsync(id);
            if (genre == null)
            {
                throw new KeyNotFoundException("Genre not found");
            }
            await _repository.DeleteAsync(genre.Id);
        }

        public async Task<IEnumerable<GenreResponseDto>> GetByIdsAsync(List<Guid> guids)
        {
            var genres = await _repository.GetByIdsAsync(guids) ?? [];
            return genres.Select(g => MaptoDto(g));
        }

        private GenreResponseDto MaptoDto(Genre genre)
        {
            return new GenreResponseDto
            {
                Id = genre.Id,
                Name = genre.Name
            };
        }
    }
}
