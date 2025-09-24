using library_api.Application.DTO.Book;
using library_api.Application.Interfaces;
using library_api.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace library_api.Controllers
{
    /// <summary>
    /// Controller for managing books
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService ?? throw new ArgumentNullException(nameof(bookService));
        }

        /// <summary>
        /// Get all books
        /// </summary>
        /// <returns>List of books</returns>
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<BookResponseDto>>>> GetAll()
        {
            var books = await _bookService.GetAllAsync();
            var response = ApiResponse<IEnumerable<BookResponseDto>>.SuccessResult(books);
            return Ok(response);
        }

        /// <summary>
        /// Get a book by ID
        /// </summary>
        /// <param name="id">Book ID</param>
        /// <returns>Book details</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<BookResponseDto>>> GetById(Guid id)
        {
            var book = await _bookService.GetByIdAsync(id);
            var response = ApiResponse<BookResponseDto>.SuccessResult(book!);
            return Ok(response);
        }

        /// <summary>
        /// Create a new book
        /// </summary>
        /// <param name="dto">Book creation data</param>
        /// <returns>Created book</returns>
        [HttpPost]
        public async Task<ActionResult<ApiResponse<BookResponseDto>>> Create([FromBody] CreateBookDto dto)
        {
            var book = await _bookService.CreateAsync(dto);
            var response = ApiResponse<BookResponseDto>.SuccessResult(book);
            return CreatedAtAction(nameof(GetById), new { id = book.Id }, response);
        }

        /// <summary>
        /// Update an existing book
        /// </summary>
        /// <param name="id">Book ID</param>
        /// <param name="dto">Book update data</param>
        /// <returns>Success message</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<object>>> Update(Guid id, [FromBody] UpdateBookDto dto)
        {
            await _bookService.UpdateAsync(id, dto);
            var response = ApiResponse<object>.SuccessResult(new { message = "Book updated successfully" });
            return Ok(response);
        }

        /// <summary>
        /// Delete a book
        /// </summary>
        /// <param name="id">Book ID</param>
        /// <returns>Success message</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<object>>> Delete(Guid id)
        {
            await _bookService.DeleteAsync(id);
            var response = ApiResponse<object>.SuccessResult(new { message = "Book deleted successfully" });
            return Ok(response);
        }

        /// <summary>
        /// Search for books
        /// </summary>
        /// <param name="query">Search query</param>
        /// <returns>List of matching books</returns>
        [HttpGet("search")]
        public async Task<ActionResult<ApiResponse<IEnumerable<BookResponseDto>>>> Search([FromQuery] string query)
        {
            var books = await _bookService.SearchAsync(query);
            var response = ApiResponse<IEnumerable<BookResponseDto>>.SuccessResult(books);
            return Ok(response);
        }

        /// <summary>
        /// Get all available books
        /// </summary>
        /// <returns>List of available books</returns>
        [HttpGet("available")]
        public async Task<ActionResult<ApiResponse<IEnumerable<BookResponseDto>>>> GetAvailable()
        {
            var books = await _bookService.GetAvailableAsync();
            var response = ApiResponse<IEnumerable<BookResponseDto>>.SuccessResult(books);
            return Ok(response);
        }

        /// <summary>
        /// Get books by author
        /// </summary>
        /// <param name="authorId">Author ID</param>
        /// <returns>List of books by the author</returns>
        [HttpGet("by-author/{authorId}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<BookResponseDto>>>> GetByAuthor(Guid authorId)
        {
            var books = await _bookService.GetByAuthorAsync(authorId);
            var response = ApiResponse<IEnumerable<BookResponseDto>>.SuccessResult(books);
            return Ok(response);
        }

        /// <summary>
        /// Get books by genre
        /// </summary>
        /// <param name="genreId">Genre ID</param>
        /// <returns>List of books in the genre</returns>
        [HttpGet("by-genre/{genreId}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<BookResponseDto>>>> GetByGenre(Guid genreId)
        {
            var books = await _bookService.GetByGenreAsync(genreId);
            var response = ApiResponse<IEnumerable<BookResponseDto>>.SuccessResult(books);
            return Ok(response);
        }
    }
}
