using Biblioteka.DataBaseContext;
using Biblioteka.Interfaces;
using Biblioteka.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Reflection.PortableExecutable;

namespace Biblioteka.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReaderController : Controller
    {
        private readonly IReaderInterface _readerInterface;
        public ReaderController(IReaderInterface readerInterface) 
        {
            _readerInterface = readerInterface;
        }

        [HttpGet]
        public async Task<ActionResult<Readers>> GetReader(int? registrationDate = null)
        {
            return await _readerInterface.GetReader(registrationDate);
        }

        [HttpPost]
        public async Task<ActionResult<Readers>> PostReader(Readers readers)
        {
            return await _readerInterface.PostReader(readers);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Readers>> GetReader(int id)
        {
            return await _readerInterface.GetReader(id);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, Readers reader)
        {
            return await _readerInterface.PutBook(id, reader);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReader(int id)
        {
            return await _readerInterface.DeleteReader(id);
        }
        [HttpGet("books")]
        public async Task<ActionResult<Books>> GetBooksByReader(int id)
        {
            return await _readerInterface.GetBooksByReader(id);
        }
    }
}
