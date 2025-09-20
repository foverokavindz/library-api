using library_api.Application.DTO.Book;
using library_api.Application.Interfaces;
using library_api.Domain.Entities;
using library_api.Domain.Interfaces;

namespace library_api.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IGenreRepository _genreRepository;

        public BookService(IBookRepository bookRepository, IAuthorRepository authorRepository, IGenreRepository genreRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _genreRepository = genreRepository;
        }

        public async Task<BookDto> CreateAsync(CreateBookDto dto)
        {
            // Map Authors and Genres
            var authors = await _authorRepository.GetByIdsAsync(dto.AuthorIds) ?? [];
            var genres = await _genreRepository.GetByIdsAsync(dto.GenreIds) ?? [];

            // Create Entity
            var book = new Book
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                Description = dto.Description,
                YearPublished = dto.YearPublished,
                ISBN = dto.ISBN,
                Language = dto.Language,
                IsAvailable = true,
                AvailableCopies = 0,
                Authors = [.. authors], // Set required property in initializer
                Genres = [.. genres]
            };

            await _bookRepository.AddAsync(book);

            return MapToDto(book);
        }

        public async Task<IEnumerable<BookDto>> GetAllAsync()
        {
            var books = await _bookRepository.GetAllAsync();

            return books.Select(b => MapToDto(b));
        }

        public async Task<BookDto?> GetByIdAsync(Guid id) { 
            var book = await _bookRepository.GetByIdAsync(id);
            return book == null ? null : MapToDto(book);
        }

        public async Task UpdateAsync(Guid id, UpdateBookDto dto)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null) throw new Exception("Book not found");

            book.Title = dto.Title;
            book.Description = dto.Description;
            book.YearPublished = dto.YearPublished;
            book.ISBN = dto.ISBN;
            book.Language = dto.Language;

            var authors = await _authorRepository.GetByIdsAsync(dto.AuthorIds) ?? [];
            book.Authors = authors.ToList();

            var genres = await _genreRepository.GetByIdsAsync(dto.GenreIds) ?? [];
            book.Genres = genres.ToList();

            await _bookRepository.UpdateAsync(book);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _bookRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<BookDto>> SearchAsync(string query)
        {
            var books = await _bookRepository.SearchAsync(query);
            return books.Select(MapToDto);
        }

        public async Task<IEnumerable<BookDto>> GetAvailableAsync()
        {
            var books = await _bookRepository.GetAvailableAsync();
            return books.Select(MapToDto);
        }

        public async Task<IEnumerable<BookDto>> GetAuthorsAsync(Guid bookId)
        {
            var books = await _bookRepository.GetByAuthorAsync(bookId);
            return books.Select(MapToDto);
        }

        public async Task<IEnumerable<BookDto>> GetGenresAsync(Guid bookId)
        {
            var books = await _bookRepository.GetByGenreAsync(bookId);
            return books.Select(MapToDto);
        }

        public async Task<bool> BorrowAsync(Guid userId, Guid bookId) // in here find out how can i return meannig  full message // also if it false there should be good way to determine it from the  controller side
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return false;
            }

            var book = await _bookRepository.GetByIdAsync(bookId);
            if (book == null)
            {
                return false;
            }
            if (book.AvailableCopies <= 0)
            {
                return false;
            } else if (book.IsAvailable == false)
            {
                return false;

            } else
            {

            }

                private BookDto MapToDto(Book book)
                {
                    return new BookDto
                    {
                        Id = book.Id,
                        Title = book.Title,
                        Description = book.Description,
                        YearPublished = book.YearPublished,
                        IsAvailable = book.IsAvailable,
                        AvailableCopies = book.AvailableCopies,
                        Language = book.Language,
                        Authors = book.Authors.Select(a => $"{a.FirstName} {a.LastName}"),
                        Genres = book.Genres?.Select(g => g.Name) ?? new List<string>()
                    };
                }

    }
}
