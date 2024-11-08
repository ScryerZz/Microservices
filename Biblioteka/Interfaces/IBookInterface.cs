using Biblioteka.Model;
using Biblioteka.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteka.Interfaces
{
    public interface IBookInterface
    {
        Task<ActionResult<Books>> GetBooks([FromQuery] string? author, [FromQuery] string? genre, [FromQuery] int? year, [FromQuery] int? page, [FromQuery] int? pageSize);
        Task<ActionResult<Books>> GetBook(int id);
        Task<ActionResult<Books>> GetBooksByGenre(int id);
        Task<ActionResult<Books>> GetBookByAuthor(string author);
        Task<IActionResult> AddNewBook(CreateBook book);
        Task<IActionResult> PutBook(int id, [FromBody] Books book);
        Task<IActionResult> DeleteBook(int id);
        Task<ActionResult<int>> GetAvailableCopies(int id);
    }
}
