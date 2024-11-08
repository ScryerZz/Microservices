using Biblioteka.DataBaseContext;
using Biblioteka.Interfaces;
using Biblioteka.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Biblioteka.Services
{
    public class RentalService : IRentalInterface
    {
        private readonly TestApiDb _context;

        public RentalService(TestApiDb context)
        {
            _context = context;
        }
        public async Task<ActionResult<Rental>> GetRental()
        {
            var rental = await _context.Rental.ToListAsync();
            return new ObjectResult(rental);
        }
        public async Task<ActionResult<Rental>> GetBookByStatus(string status)
        {
            var nameParts = status.ToLower().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            var rental = await _context.Rental.Where(i => nameParts.All(part => i.status_rental.ToLower().Contains(part))).ToListAsync();

            return new ObjectResult(rental);
        }
        public async Task<ActionResult<Rental>> GetBooksByReader(int id)
        {
            var rental = await _context.Readers.FirstOrDefaultAsync(g => g.Id == id);
            if (rental == null)
            {
                return new OkObjectResult(new { Message = "ID истории не совпадает." });
            }
            return new OkObjectResult(_context.Rental.Where(i => i.reader_id == id));
        }
        public async Task<ActionResult<Rental>> GetBooksByBook(int id)
        {
            var rental = await _context.Books.FirstOrDefaultAsync(g => g.Id == id);
            if (rental == null)
            {
                return new OkObjectResult(new { Message = "ID истории не совпадает." });
            }
            return new OkObjectResult(_context.Rental.Where(i => i.books_id == id));
        }
        public async Task<ActionResult<Rental>> PostBook([FromBody] Rental rental)
        {
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(rental, new ValidationContext(rental), validationResults);
            if (!isValid)
            {
                return new OkObjectResult(new { Message = "ID истории не совпадает." });
            }

            await _context.Rental.AddAsync(rental);
            await _context.SaveChangesAsync();

            return new OkResult();
        }
        public async Task<IActionResult> PutRental(int id, [FromBody] Rental rental)
        {
            if (id != rental.Id)
            {
                return new OkObjectResult(new { Message = "ID истории не совпадает." });
            }

            _context.Entry(rental).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return new OkObjectResult(new { Message = "ID истории не совпадает." });
            }

            return new OkResult();
        }

        public async Task<IActionResult> DeleteRental(int id)
        {

            var rental = await _context.Rental.FindAsync(id);
            if (rental == null)
            {
                return new OkObjectResult(new { Message = "ID истории не совпадает." });
            }

            _context.Rental.Remove(rental);
            await _context.SaveChangesAsync();

            return new OkResult();
        }
    }
}
