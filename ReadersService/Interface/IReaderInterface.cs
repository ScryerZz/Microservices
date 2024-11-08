using Biblioteka.Model;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteka.Interfaces
{
    public interface IReaderInterface
    {
        Task<ActionResult<Readers>> GetReader(int? registrationDate);
        Task<ActionResult<Readers>> PostReader(Readers readers);
        Task<ActionResult<Readers>> GetReader(int id);
        Task<IActionResult> PutBook(int id, Readers reader);
        Task<IActionResult> DeleteReader(int id);
        Task<ActionResult<Books>> GetBooksByReader(int id);
    }
}
