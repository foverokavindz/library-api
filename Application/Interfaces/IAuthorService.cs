using library_api.Application.DTO.Author;
using library_api.Application.DTO.Book;
using library_api.Domain.Interfaces;

namespace library_api.Application.Interfaces
{
    public interface IAuthorService : IBaseService<AuthorDto, CreateAuthorDto, UpdateAuthorDto>
    {
        Task<IEnumerable<AuthorDto>> GetByIdsAsync(List<Guid> guids);
        Task<IEnumerable<AuthorDto>> SearchAsync(string query);
    }
}
