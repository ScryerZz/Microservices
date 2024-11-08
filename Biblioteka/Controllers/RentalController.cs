using Biblioteka.DataBaseContext;
using Biblioteka.Interfaces;
using Biblioteka.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Biblioteka.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RentalController : Controller
    {
        private readonly IRentalInterface _rentalInterface;
        public RentalController(IRentalInterface rentalInterface) 
        {
            _rentalInterface = rentalInterface;
        }

        [HttpGet]
        public async Task<ActionResult<Rental>> GetRental()
        {
            return await _rentalInterface.GetRental();
        }

        [HttpGet("status")]
        public async Task<ActionResult<Rental>> GetBookByStatus(string status)
        {
            return await _rentalInterface.GetBookByStatus(status);
        }

        [HttpGet("reader")]
        public async Task<ActionResult<Rental>> GetBooksByReader(int id)
        {
            return await _rentalInterface.GetBooksByReader(id);
        }
        [HttpGet("book")]
        public async Task<ActionResult<Rental>> GetBooksByBook(int id)
        {
            return await _rentalInterface.GetBooksByBook(id);
        }

        [HttpPost]
        public async Task<ActionResult<Rental>> PostBook([FromBody] Rental rental)
        {
            return await _rentalInterface.PostBook(rental);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRental(int id, [FromBody] Rental rental)
        {
            return await _rentalInterface.PutRental(id, rental); 
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRental(int id)
        {
            return await _rentalInterface.DeleteRental(id);
        }
    }
}
