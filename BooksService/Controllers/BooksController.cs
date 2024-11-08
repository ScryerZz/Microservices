using Biblioteka.DataBaseContext;
using Biblioteka.Model;
using Biblioteka.Requests;
using Biblioteka.Services;
using BooksService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Biblioteka.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : Controller
    {
        private readonly IBookInterface _bookService;

        public BooksController(IBookInterface bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult<Books>> GetBooks([FromQuery] string? author, [FromQuery] string? genre, [FromQuery] int? year, [FromQuery] int? page,[FromQuery] int? pageSize)
        {
            return await _bookService.GetBooks(author, genre, year, page, pageSize);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Books>> GetBook(int id)
        { 
            return await _bookService.GetBook(id);
        }

        [HttpGet("genre")]
        public async Task<ActionResult<Books>> GetBooksByGenre(int id)
        {
            return await _bookService.GetBooksByGenre(id);
        }

        [HttpGet("author")]
        public async Task<ActionResult<Books>> GetBookByAuthor(string author)
        {
            return await _bookService.GetBookByAuthor(author); 
        }

        [HttpPost]
        public async Task<IActionResult> AddNewBook(CreateBook book)
        {
            return await _bookService.AddNewBook(book);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, [FromBody] Books book)
        {
            return await _bookService.PutBook(id, book);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            return await _bookService.DeleteBook(id);
        }

        [HttpGet("{id}/availability")]
        public async Task<ActionResult<int>> GetAvailableCopies(int id)
        {
            return await _bookService.GetAvailableCopies(id);
        }
    }
}
