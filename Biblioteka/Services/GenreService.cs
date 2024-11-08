using Biblioteka.DataBaseContext;
using Biblioteka.Interfaces;
using Biblioteka.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Biblioteka.Services
{
    public class GenreService : IGenreInterface
    {
        private readonly TestApiDb _context;

        public GenreService(TestApiDb context) 
        {
            _context = context;
        }
        public async Task<ActionResult<Genre>> GetGenre()
        {
            var genres = await _context.Genres.ToListAsync();
            return new ObjectResult(genres);
        }

        public async Task<ActionResult<Genre>> PostBook([FromBody] Genre genre)
        {
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(genre, new ValidationContext(genre), validationResults);
            if (!isValid)
            {
                return new OkObjectResult(new { error = "error" });
            }

            await _context.Genres.AddAsync(genre);
            await _context.SaveChangesAsync();

            return new OkResult();
        }

        public async Task<IActionResult> PutBook(int id, [FromBody] Genre genre)
        {
            if (id != genre.Id)
            {
                return new OkObjectResult(new { Message = "ID жанра не совпадает." });
            }

            _context.Entry(genre).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return new OkObjectResult(new { error = "error" });
            }

            return new OkResult(); 
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre == null)
            {
                return new OkObjectResult(new { Message = "ID жанра не совпадает." });
            }

            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();

            return new OkResult();
        }
    }
}
