using Biblioteka.Model;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteka.Interfaces
{
    public interface IPhotoInterface
    {
        Task<IActionResult> UploadPhoto(IFormFile file);
        ActionResult<IEnumerable<Photo>> GetPhotos();
        ActionResult<Photo> GetPhoto(int id);
        Task<IActionResult> UpdatePhoto(int id, [FromBody] Photo updatedPhoto);
        Task<IActionResult> DeletePhoto(int id);
    }
}
