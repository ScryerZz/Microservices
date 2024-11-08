using Biblioteka.DataBaseContext;
using Biblioteka.Interfaces;
using Biblioteka.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Biblioteka.Services
{
    public class PhotoServices : IPhotoInterface
    {
        private readonly TestApiDb _context;
        private readonly string _uploadPath;

        public PhotoServices(TestApiDb context)
        {
            _context = context;
            _uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedPhotos");
            if (!Directory.Exists(_uploadPath))
            {
                Directory.CreateDirectory(_uploadPath);
            }
        }
        [HttpPost("upload")]
        public async Task<IActionResult> UploadPhoto(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return new OkObjectResult(new { Message = "error" });
            }

            var fileName = Path.GetFileName(file.FileName);
            var filePath = Path.Combine(_uploadPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var photo = new Photo { FileName = fileName, FilePath = filePath };
            _context.Photos.Add(photo);
            await _context.SaveChangesAsync();

            return new OkResult();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Photo>> GetPhotos()
        {
            return new OkObjectResult(_context.Photos.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<Photo> GetPhoto(int id)
        {
            var photo = _context.Photos.Find(id);
            if (photo == null)
            {
                return new OkObjectResult(new { Message = "error" }); ;
            }

            return new OkObjectResult(photo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePhoto(int id, [FromBody] Photo updatedPhoto)
        {
            if (id != updatedPhoto.Id)
            {
                return new OkObjectResult(new { Message = "error" });
            }

            _context.Entry(updatedPhoto).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Photos.Any(e => e.Id == id))
                {
                    return new OkObjectResult(new { Message = "error" });
                }
                else
                {
                    throw;
                }
            }

            return new OkObjectResult(new { Message = "error" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhoto(int id)
        {
            var photo = await _context.Photos.FindAsync(id);
            if (photo == null)
            {
                return new OkObjectResult(new { Message = "error" });
            }

            _context.Photos.Remove(photo);
            await _context.SaveChangesAsync();

            var filePath = Path.Combine(_uploadPath, photo.FileName);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            return new OkObjectResult(new { Message = "error" });
        }
    }
}
