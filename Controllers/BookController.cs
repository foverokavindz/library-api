using library_api.Application.DTO.Book;
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
                .Select(b => new BookDto
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var book = await _bookService.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            var result = new BookDto
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
            var book = await _bookService.CreateBookAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);
        }

    }
}
