using library_api.Application.DTO.Book;
using library_api.Application.Interfaces;
using library_api.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace library_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<BookResponseDto>>>> GetAll()
        {
            var books = await _bookService.GetAllAsync();
            var response = ApiResponse<IEnumerable<BookResponseDto>>.SuccessResponse(books);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<BookResponseDto>>> GetById(Guid id)
        {
            var book = await _bookService.GetByIdAsync(id);
            var response = ApiResponse<BookResponseDto>.SuccessResponse(book!);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<BookResponseDto>>> Create([FromBody] CreateBookDto dto)
        {
            var book = await _bookService.CreateAsync(dto);
            var response = ApiResponse<BookResponseDto>.SuccessResponse(book);
            return CreatedAtAction(nameof(GetById), new { id = book.Id }, response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<object>>> Update(Guid id, [FromBody] UpdateBookDto dto)
        {
            await _bookService.UpdateAsync(id, dto);
            var response = ApiResponse<object>.SuccessResponse(new { message = "Book updated successfully" });
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<object>>> Delete(Guid id)
        {
            await _bookService.DeleteAsync(id);
            var response = ApiResponse<object>.SuccessResponse(new { message = "Book deleted successfully" });
            return Ok(response);
        }

        [HttpGet("search")]
        public async Task<ActionResult<ApiResponse<IEnumerable<BookResponseDto>>>> Search([FromQuery] string query)
        {
            var books = await _bookService.SearchAsync(query);
            var response = ApiResponse<IEnumerable<BookResponseDto>>.SuccessResponse(books);
            return Ok(response);
        }

        [HttpGet("available")]
        public async Task<ActionResult<ApiResponse<IEnumerable<BookResponseDto>>>> GetAvailable()
        {
            var books = await _bookService.GetAvailableAsync();
            var response = ApiResponse<IEnumerable<BookResponseDto>>.SuccessResponse(books);
            return Ok(response);
        }

        [HttpGet("by-author/{authorId}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<BookResponseDto>>>> GetByAuthor(Guid authorId)
        {
            var books = await _bookService.GetByAuthorAsync(authorId);
            var response = ApiResponse<IEnumerable<BookResponseDto>>.SuccessResponse(books);
            return Ok(response);
        }

        [HttpGet("by-genre/{genreId}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<BookResponseDto>>>> GetByGenre(Guid genreId)
        {
            var books = await _bookService.GetByGenreAsync(genreId);
            var response = ApiResponse<IEnumerable<BookResponseDto>>.SuccessResponse(books);
            return Ok(response);
        }
    }
}
