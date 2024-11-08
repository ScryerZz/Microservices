using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biblioteka.Model
{
    public class Books
    {
        [Key]
        public int Id { get; set; } 
        public string Title { get; set; }
        public string? Author { get; set; } 
        public int PublicationYear { get; set; } 
        public string Description { get; set; } 
        public int? AvailableCopies { get; set; } 

        [Required]
        [ForeignKey(nameof(Genre))]
        public int? Genre_id { get; set; }
        public Genre? Genre { get; set; }

        [Required]
        [ForeignKey("Readers")]
        public int? Readers_id { get; set; }

    }
}
