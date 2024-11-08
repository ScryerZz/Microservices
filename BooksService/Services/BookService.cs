using Biblioteka.DataBaseContext;
using Biblioteka.Model;
using Biblioteka.Requests;
using BooksService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Biblioteka.Services
{
    public class BookService : IBookInterface
    {
        private readonly TestApiDb _context;    

        public BookService(TestApiDb context)
        {
            _context = context;
        }

        public async Task<ActionResult<Books>> GetBooks([FromQuery] string? author, [FromQuery] string? genre, [FromQuery] int? year, [FromQuery] int? page, [FromQuery] int? pageSize)
        {
            //IQueryable<Books> query = _context.Books.Include(b => b.Genre);

            var query = _context.Books.AsQueryable();

            if (!string.IsNullOrEmpty(author))
                query = query.Where(b => b.Author == author);

            if (!string.IsNullOrEmpty(genre))
                query = query.Where(b => b.Genre.Name == genre);

            if (year.HasValue)
                query = query.Where(b => b.PublicationYear == year);

            var totalItems = await query.CountAsync();
            var books = await query.Skip((int)((page - 1) * pageSize)).Take((int)pageSize).ToListAsync();

            return new OkObjectResult(new
            {
                TotalItems = totalItems,
                Page = page,
                PageSize = pageSize,
                Books = books
            });
        }

        public async Task<ActionResult<Books>> GetBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return new OkObjectResult(new {error = "Такой книги нет"});
            }
            return new ObjectResult(book);
        }

        public async Task<ActionResult<Books>> GetBooksByGenre(int id)
        {
            var genre = await _context.Genres.FirstOrDefaultAsync(g => g.Id == id);
            if (genre == null)
            {
                return new OkObjectResult(new { error = "Такой книги нет" });
            }
            return new OkObjectResult(_context.Books.Where(i => i.Genre_id == id));
        }
        public async Task<ActionResult<Books>> GetBookByAuthor(string author)
        {
            var nameParts = author.ToLower().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            var books = await _context.Books.Where(i => nameParts.All(part => i.Author.ToLower().Contains(part))).ToListAsync();

            return new OkObjectResult(books);
        }
        public async Task<IActionResult> AddNewBook(CreateBook book)
        {

            if (string.IsNullOrWhiteSpace(book.Title)
                || string.IsNullOrWhiteSpace(book.Author)
                || string.IsNullOrWhiteSpace(book.Description)
                || string.IsNullOrWhiteSpace(Convert.ToString(book.Genre_id))
                || string.IsNullOrWhiteSpace(Convert.ToString(book.Reader_id))
                || string.IsNullOrWhiteSpace(Convert.ToString(book.PublicationYear))
                || string.IsNullOrWhiteSpace(Convert.ToString(book.AvailableCopies)))
            {
                return new OkObjectResult(new
                {
                    error = ""
                });
            }
            var temp = await _context.Books.FirstOrDefaultAsync(b => b.Title == book.Title && b.Author == book.Author);
            if (temp != null)
            {
                return new OkObjectResult(new
                {
                    error = ""
                });
            }
            var Book = new Books()
            {
                Title = book.Title,
                Author = book.Author,
                Description = book.Description,
                PublicationYear = book.PublicationYear,
                Genre_id = book.Genre_id,
                Readers_id = book.Reader_id,
                AvailableCopies = book.AvailableCopies
            };
            await _context.Books.AddAsync(Book);
            await _context.SaveChangesAsync();
            return new OkResult();
        }
        public async Task<IActionResult> PutBook(int id, [FromBody] Books book)
        {
            if (id != book.Id)
            {
                return new OkObjectResult(new { Message = "ID книги не совпадает." });
            }

            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return new OkObjectResult(new { Message = "ID книги не совпадает." });
            }

            return new OkResult();
        }
        public async Task<IActionResult> DeleteBook(int id)
        {

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return new OkObjectResult(new { Message = "ID книги не совпадает." });
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return new OkResult();
        }

        [HttpGet("{id}/availability")]
        public async Task<ActionResult<int>> GetAvailableCopies(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return new OkObjectResult(new { Message = "Таких копий нет" });
            return book.AvailableCopies;
        }
    }
}
