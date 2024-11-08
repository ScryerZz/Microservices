using Biblioteka.DataBaseContext;
using Biblioteka.Interfaces;
using Biblioteka.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Biblioteka.Services
{
    public class ReaderService : IReaderInterface
    {
        private readonly TestApiDb _context;

        public ReaderService(TestApiDb context)
        {
            _context = context;
        }
        public async Task<ActionResult<Readers>> GetReader(int? registrationDate)
        {
            var query = _context.Readers.AsQueryable();

            if (registrationDate.HasValue)
            {
                query = query.Where(r => r.RegistrationDate == registrationDate.Value);
            }

            return new OkObjectResult(query);
        }
        public async Task<ActionResult<Readers>> PostReader(Readers readers)
        {
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(readers, new ValidationContext(readers), validationResults);
            if (!isValid)
            {
                return new OkObjectResult(new { Message = "ID читателя не совпадает." });
            }

            await _context.Readers.AddAsync(readers);
            await _context.SaveChangesAsync();

            return new OkResult();
        }
        public async Task<ActionResult<Readers>> GetReader(int id)
        {
            var reader = await _context.Readers.FindAsync(id);
            if (reader == null)
            {
                return new OkObjectResult(new { Message = "ID читателя не совпадает." });
            }
            return new OkObjectResult(reader);
        }
        public async Task<IActionResult> PutBook(int id, Readers reader)
        {
            if (id != reader.Id)
            {
                return new OkObjectResult(new { Message = "ID читателя не совпадает." });
            }

            _context.Entry(reader).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return new OkObjectResult(new { Message = "ID читателя не совпадает." });
            }

            return new OkResult();
        }
        public async Task<IActionResult> DeleteReader(int id)
        {

            var reader = await _context.Readers.FindAsync(id);
            if (reader == null)
            {
                return new OkObjectResult(new { Message = "ID читателя не совпадает." });
            }

            _context.Readers.Remove(reader);
            await _context.SaveChangesAsync();

            return new OkResult();
        }
        public async Task<ActionResult<Books>> GetBooksByReader(int id)
        {
            var book = await _context.Readers.FirstOrDefaultAsync(g => g.Id == id);
            if (book == null)
            {
                return new OkObjectResult(new { Message = "ID читателя не совпадает." });
            }
            return new OkObjectResult(_context.Books.Where(i => i.Readers_id == id));
        }
    }
}
