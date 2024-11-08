using System.ComponentModel.DataAnnotations;

namespace Biblioteka.Model
{
    public class Photo
    {
        [Key]
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
    }
}
