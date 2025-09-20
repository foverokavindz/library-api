using library_api.Application.DTO.Author;
using library_api.Application.DTO.Genre;
using library_api.Application.Interfaces;
using library_api.Domain.Entities;
using library_api.Domain.Interfaces;

namespace library_api.Application.Services
{
    // TODO : Books are not handled here
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _repository;

        public AuthorService(IAuthorRepository repository)
        {
            _repository = repository;
        }

        public async Task<AuthorResponseDto> GetByIdAsync(Guid id)
        {
            var author = await _repository.GetByIdAsync(id);
            return author == null ? null : MapToDto(author);
        }

        public async Task<IEnumerable<AuthorResponseDto>> GetAllAsync()
        {
            var authors = await _repository.GetAllAsync();
            return authors.Select(a => MapToDto(a));
        }

        public async Task DeleteAsync(Guid id)
        {
            var author = await _repository.GetByIdAsync(id);
            if (author == null)
            {
                throw new KeyNotFoundException("Author not found");
            }
            await _repository.DeleteAsync(author.Id);
        }

        public async Task<AuthorResponseDto> CreateAsync(CreateAuthorDto dto)
        {
            var author = new Author
            {
                Id = Guid.NewGuid(),
                FirstName = dto.FirstName,
                LastName = dto.LastName,

            };
            await _repository.AddAsync(author);
            return MapToDto(author);
        }

        public async Task UpdateAsync(Guid id, UpdateAuthorDto dto)
        {
            var author = await _repository.GetByIdAsync(id);
            if (author == null)
            {
                throw new KeyNotFoundException("Author not found");
            }
            author.FirstName = dto.FirstName;
            author.LastName = dto.LastName;
            await _repository.UpdateAsync(author);
        }

        public async Task<IEnumerable<AuthorResponseDto>> GetByIdsAsync(List<Guid> guids)
        {
            var authors = await _repository.GetByIdsAsync(guids);
            return authors.Select(a => MapToDto(a));
        }

        public async Task<IEnumerable<AuthorResponseDto>> SearchAsync(string query)
        {
            var authors = await _repository.SearchAsync(query);
            return authors.Select(a => MapToDto(a));
        }

        private AuthorResponseDto MapToDto(Author author)
        {
            return new AuthorResponseDto
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName
            };
        }
    }
}
