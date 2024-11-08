using Biblioteka.Model;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteka.Interfaces
{
    public interface IGenreInterface
    {
        Task<ActionResult<Genre>> GetGenre();
        Task<ActionResult<Genre>> PostBook([FromBody] Genre genre);
        Task<IActionResult> PutBook(int id, [FromBody] Genre genre);
        Task<IActionResult> DeleteGenre(int id);
    }
}
