using Biblioteka.Model;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteka.Interfaces
{
    public interface IRentalInterface
    {
        Task<ActionResult<Rental>> GetRental();
        Task<ActionResult<Rental>> GetBookByStatus(string status);
        Task<ActionResult<Rental>> GetBooksByReader(int id);
        Task<ActionResult<Rental>> GetBooksByBook(int id);
        Task<ActionResult<Rental>> PostBook([FromBody] Rental rental);
        Task<IActionResult> PutRental(int id, [FromBody] Rental rental);
        Task<IActionResult> DeleteRental(int id);
    }
}
