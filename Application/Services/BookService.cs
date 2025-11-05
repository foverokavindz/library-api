using library_api.Application.DTO.Book;
using library_api.Application.DTO.Author;
using library_api.Application.DTO.Genre;
using library_api.Application.Interfaces;
using library_api.Domain.Exceptions;
using library_api.Domain.Entities;
using library_api.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace library_api.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IGenreRepository _genreRepository;

        public BookService(IBookRepository bookRepository, IAuthorRepository authorRepository, IGenreRepository genreRepository)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
            _authorRepository = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository));
            _genreRepository = genreRepository ?? throw new ArgumentNullException(nameof(genreRepository));
        }

        public async Task<BookResponseDto> CreateAsync(CreateBookDto dto)
        {
            if (dto == null)
                throw new ValidationException("Book creation data cannot be null.");

            if (string.IsNullOrWhiteSpace(dto.Title))
                throw new ValidationException("Book title is required.");

            // Check if book with same ISBN already exists
            if (!string.IsNullOrEmpty(dto.ISBN))
            {
                var exists = await _bookRepository
                    .AsQueryable() // use AsQueryable to build the query efficiently
                    .AnyAsync(b => b.ISBN == dto.ISBN);

                if (exists)
                    throw new ConflictException($"A book with ISBN '{dto.ISBN}' already exists.");
            }

            // Check author and genre is null

            if (dto.AuthorIds.Count != 0)
            {
                var authorIds = (dto.Authors ?? Enumerable.Empty<CreateAuthorDto>()).Where(a => a.Id.HasValue).Select(a => a.Id!.Value).ToList(); 
                if (authorIds.Count != 0)
                {
                    var existingAuthors = await _authorRepository.GetByIdsAsync(authorIds);
                    if (existingAuthors.Count() != authorIds.Count)
                        throw new ValidationException("One or more specified authors do not exist.");
                }

                var newAuthors = (dto.Authors ?? Enumerable.Empty<CreateAuthorDto>()).Where(a => a.Id == null && (string.IsNullOrWhiteSpace(a.FirstName) || string.IsNullOrWhiteSpace(a.LastName))).ToList();
                if (newAuthors.Count != 0)
                {
                    foreach (var newAuthorDto in newAuthors)
                    {
                        var newAuthor = new Author
                        {
                            Id = Guid.NewGuid(),
                            FirstName = newAuthorDto.FirstName,
                            LastName = newAuthorDto.LastName,
                            Bio = newAuthorDto.Bio
                        };
                        await _authorRepository.AddAsync(newAuthor);
                    }
                }
            }


            var genreIds = dto.Genres?.Select(g => g?.Id).ToList() ?? null;

            // if exists

            // save new 

            // Map Authors and Genres
            var authors = await _authorRepository.GetByIdsAsync(dto.AuthorIds)!;
            if (authors == null || !authors.Any())
                throw new ValidationException("At least one valid author must be specified.");

            var genres = await _genreRepository.GetByIdsAsync(dto.GenreIds)!;
            if (genres == null || !genres.Any())
                throw new ValidationException("At least one valid genre must be specified.");

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
                AvailableCopies = 1,
                Authors = authors.ToList(),
                Genres = genres.ToList()
            };

            try
            {
                await _bookRepository.AddAsync(book);
                return MapToDto(book);
            }
            catch (Exception ex)
            {
                // Wrap any unexpected database or system exceptions into a domain-level error 
                // to keep business logic isolated and prevent leaking internal details.
                throw new InvalidOperationException($"Failed to create book: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<BookResponseDto>> GetAllAsync()
        {
            try
            {
                var books = await _bookRepository.GetAllAsync();
                return books?.Where(b => b != null).Select(MapToDto) ?? Enumerable.Empty<BookResponseDto>();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to retrieve books: {ex.Message}", ex);
            }
        }

        public async Task<BookResponseDto?> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ValidationException("Book ID cannot be empty.");

            try
            {
                var book = await _bookRepository.GetByIdAsync(id);
                if (book == null)
                    throw new BookNotFoundException(id);

                return MapToDto(book);
            }
            catch (BookNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to retrieve book: {ex.Message}", ex);
            }
        }

        public async Task UpdateAsync(Guid id, UpdateBookDto dto)
        {
            if (id == Guid.Empty)
                throw new ValidationException("Book ID cannot be empty.");

            if (dto == null)
                throw new ValidationException("Book update data cannot be null.");

            if (string.IsNullOrWhiteSpace(dto.Title))
                throw new ValidationException("Book title is required.");

            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
                    throw new BookNotFoundException(id);

            // Check if ISBN is being changed to one that already exists
            if (!string.IsNullOrEmpty(dto.ISBN) && book.ISBN != dto.ISBN)
            {
                var existingBooks = await _bookRepository.GetAllAsync();
                if (existingBooks?.Any(b => b?.ISBN == dto.ISBN && b.Id != id) == true)
                    throw new ConflictException($"A book with ISBN '{dto.ISBN}' already exists.");
            }

            book.Title = dto.Title;
            book.Description = dto.Description;
            book.YearPublished = dto.YearPublished;
            book.ISBN = dto.ISBN;
            book.Language = dto.Language;

            var authors = await _authorRepository.GetByIdsAsync(dto.AuthorIds)!;
            if (authors == null || !authors.Any())
                throw new ValidationException("At least one valid author must be specified.");
            book.Authors = authors.ToList();

            var genres = await _genreRepository.GetByIdsAsync(dto.GenreIds)!;
            if (genres == null || !genres.Any())
                throw new ValidationException("At least one valid genre must be specified.");
            book.Genres = genres.ToList();

            try
            {
                await _bookRepository.UpdateAsync(book);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to update book: {ex.Message}", ex);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ValidationException("Book ID cannot be empty.");

            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
                    throw new BookNotFoundException(id);

            try
            {
                await _bookRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to delete book: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<BookResponseDto>> SearchAsync(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                throw new ValidationException("Search query cannot be empty.");

            try
            {
                var books = await _bookRepository.SearchAsync(query)!;
                return books?.Where(b => b != null).Select(MapToDto) ?? Enumerable.Empty<BookResponseDto>();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to search books: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<BookResponseDto>> GetAvailableAsync()
        {
            try
            {
                var books = await _bookRepository.GetAvailableAsync()!;
                return books?.Where(b => b != null).Select(MapToDto) ?? Enumerable.Empty<BookResponseDto>();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to retrieve available books: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<BookResponseDto>> GetByAuthorAsync(Guid authorId)
        {
            if (authorId == Guid.Empty)
                throw new ValidationException("Author ID cannot be empty.");

            try
            {
                var books = await _bookRepository.GetByAuthorAsync(authorId);
                return books?.Where(b => b != null).Select(MapToDto) ?? Enumerable.Empty<BookResponseDto>();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to retrieve books by author: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<BookResponseDto>> GetByGenreAsync(Guid genreId)
        {
            if (genreId == Guid.Empty)
                throw new ValidationException("Genre ID cannot be empty.");

            try
            {
                var books = await _bookRepository.GetByGenreAsync(genreId)!;
                return books?.Where(b => b != null).Select(MapToDto) ?? Enumerable.Empty<BookResponseDto>();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to retrieve books by genre: {ex.Message}", ex);
            }
        }

        private static BookResponseDto MapToDto(Book? book)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book), "Book cannot be null.");

            return new BookResponseDto
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                YearPublished = book.YearPublished,
                ISBN = book.ISBN,
                IsAvailable = book.IsAvailable,
                AvailableCopies = book.AvailableCopies,
                Language = book.Language,
                Authors = book.Authors?.Select(a => new AuthorResponseDto
                {
                    Id = a.Id,
                    FirstName = a.FirstName,
                    LastName = a.LastName
                }).ToList() ?? new List<AuthorResponseDto>(),
                Genres = book.Genres?.Select(g => new GenreResponseDto
                {
                    Id = g.Id,
                    Name = g.Name
                }).ToList() ?? new List<GenreResponseDto>()
            };
        }
    }
}
