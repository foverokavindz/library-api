using library_api.Application.DTO.Author;
using library_api.Application.DTO.Book;
using library_api.Application.DTO.Genre;
using library_api.Application.Interfaces;
using library_api.Domain.Entities;
using library_api.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace library_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : Controller
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService) {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var books = await _bookService.GetAllAsync();

            var result = books
                .Select(b => new BookResponseDto
                {
                    Id = b!.Id,
                    Title = b.Title,
                    Description = b.Description,
                    YearPublished = b.YearPublished,
                    IsAvailable = b.IsAvailable,
                    AvailableCopies = b.AvailableCopies,
                    Language = b.Language,
                    Authors = b.Authors?.Select(a => new AuthorResponseDto
                    {
                        Id = a.Id,
                        FirstName = a.FirstName,
                        LastName = a.LastName
                    }).ToList() ?? new List<AuthorResponseDto>(),
                    Genres = b.Genres?.Select(g => new GenreResponseDto
                    {
                        Id = g.Id,
                        Name = g.Name
                    }).ToList() ?? new List<GenreResponseDto>()
                });

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var book = await _bookService.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            var result = new BookResponseDto
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                YearPublished = book.YearPublished,
                IsAvailable = book.IsAvailable,
                AvailableCopies = book.AvailableCopies,
                Language = book.Language,
                Authors = book.Authors?.Select(a => $"{a.FirstName} {a.LastName}") ?? [],
                Genres = book.Genres?.Select(g => g.Name) ?? []
            };

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBookDto dto)
        {
            var book = await _bookService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);
        }

        // update book
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateBookDto dto) {
            var existingBook = await _bookService.GetByIdAsync(id);
            if (existingBook == null)
            {
                return NotFound();
            }
            await _bookService.UpdateAsync(id, dto);
            return NoContent();
        }

        // Delete books
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id) {
            var existingBook = await _bookService.GetByIdAsync(id);
            if (existingBook == null)
            {
                return NotFound();
            }
            await _bookService.DeleteAsync(id);
            return NoContent();
        }

        // search books
        [HttpGet("search/{query}")]
        public async Task<IActionResult> Search(string query) {
            var books = await _bookService.SearchAsync(query);
            var result = books
                .Select(b => new BookResponseDto
                {
                    Id = b!.Id,
                    Title = b.Title,
                    Description = b.Description,
                    YearPublished = b.YearPublished,
                    IsAvailable = b.IsAvailable,
                    AvailableCopies = b.AvailableCopies,
                    Language = b.Language,
                    Authors = b.Authors?.Select(a => $"{a.FirstName} {a.LastName}") ?? [],
                    Genres = b.Genres?.Select(g => g.Name) ?? []
                });
            return Ok(result);
        }

        // get available books
        [HttpGet("available")]
        public async Task<IActionResult> GetAvailable() {
            var books = await _bookService.GetAvailableAsync();
            var result = books
                .Select(b => new BookResponseDto
                {
                    Id = b!.Id,
                    Title = b.Title,
                    Description = b.Description,
                    YearPublished = b.YearPublished,
                    IsAvailable = b.IsAvailable,
                    AvailableCopies = b.AvailableCopies,
                    Language = b.Language,
                    Authors = b.Authors?.Select(a => $"{a.FirstName} {a.LastName}") ?? [],
                    Genres = b.Genres?.Select(g => g.Name) ?? []
                });
            return Ok(result);
        }

        // get book authors
        [HttpGet("{id}/authors")]
        public async Task<IActionResult> GetAuthors(Guid id) {
            var authors = await _bookService.GetAuthorsAsync(id);
            return Ok(authors);
        }

        // get book genres
        [HttpGet("{id}/genres")]
        public async Task<IActionResult> GetGenres(Guid id) {
            var genres = await _bookService.GetGenresAsync(id);
            return Ok(genres);
        }

        [HttpPost("borrow/{userId}/{bookId}")]
        public async Task<IActionResult> BorrowBook(Guid userId, Guid bookId) {

            if (userId == Guid.Empty || bookId == Guid.Empty)
            {
                return BadRequest("Invalid user ID or book ID.");
            }

            var result = await _bookService.BorrowAsync(userId, bookId);

            if (result)
            {
                return Ok("Book borrowed successfully.");
            }

            return BadRequest("Failed to borrow book.");
        }


    }
}
