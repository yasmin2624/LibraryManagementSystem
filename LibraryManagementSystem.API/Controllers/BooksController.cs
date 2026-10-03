using LibraryManagementSystem.Application.DTOs.Book;
using LibraryManagementSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace LibraryManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var books = await _bookService.GetAllAsync();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var book = await _bookService.GetByIdAsync(id);

            if (book == null)
                return NotFound();

            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBookDto dto)
        {
            var book = await _bookService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = book.BookID },
                book);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateBookDto dto)
        {
            var updated = await _bookService.UpdateAsync(id, dto);

            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleted = await _bookService.DeleteAsync(id);

                if (!deleted)
                    return NotFound();

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new
                {
                    message = ex.Message
                });
            }
        }

        [HttpGet("search/title")]
        public async Task<IActionResult> SearchByTitle(string title)
        {
            var books = await _bookService.SearchByTitleAsync(title);

            return Ok(books);
        }

        [HttpGet("search/isbn")]
        public async Task<IActionResult> SearchByISBN(string isbn)
        {
            var book = await _bookService.SearchByISBNAsync(isbn);

            if (book == null)
                return NotFound();

            return Ok(book);
        }

        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetByCategory(int categoryId)
        {
            var books = await _bookService.GetByCategoryAsync(categoryId);

            return Ok(books);
        }

        [HttpGet("author/{authorId}")]
        public async Task<IActionResult> GetByAuthor(int authorId)
        {
            var books = await _bookService.GetByAuthorAsync(authorId);

            return Ok(books);
        }

        [HttpGet("{id}/availability")]
        public async Task<IActionResult> CheckAvailability(int id)
        {
            var available = await _bookService.CheckAvailabilityAsync(id);

            return Ok(available);
        }
    }
}
