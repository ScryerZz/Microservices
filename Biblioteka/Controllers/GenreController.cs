using Biblioteka.DataBaseContext;
using Biblioteka.Interfaces;
using Biblioteka.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Biblioteka.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class GenreController : Controller
    {
        private readonly IGenreInterface _genreInterface;

        public GenreController(IGenreInterface genreInterface)
        {
            _genreInterface = genreInterface;
        }

        [HttpGet]
        public async Task<ActionResult<Genre>> GetGenre()
        {
            return await _genreInterface.GetGenre();
        }

        [HttpPost]
        public async Task<ActionResult<Genre>> PostBook([FromBody] Genre genre)
        {
            return await _genreInterface.PostBook(genre);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, [FromBody] Genre genre)
        {
            return await _genreInterface.PutBook(id, genre);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            return await _genreInterface.DeleteGenre(id);
        }
    }
}
