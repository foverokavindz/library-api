using library_api.Application.DTO.Author;
using library_api.Application.DTO.Book;
using library_api.Domain.Interfaces;

namespace library_api.Application.Interfaces
{
    public interface IAuthorService : IBaseService<AuthorResponseDto, CreateAuthorDto, UpdateAuthorDto>
    {
        Task<IEnumerable<AuthorResponseDto>> GetByIdsAsync(List<Guid> guids);
        Task<IEnumerable<AuthorResponseDto>> SearchAsync(string query);
    }
}
