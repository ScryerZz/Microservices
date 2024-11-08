using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biblioteka.Model
{
    public class Rental
    {
        [Key]
        public int Id { get; set; } 
        public DateTime RentalDate { get; set; } 
        public DateTime DueDate { get; set; } 
        public DateTime? ReturnDate { get; set; }

        [Required]
        [ForeignKey("Readers")]
        public int reader_id { get; set; }

        [Required]
        [ForeignKey("Books")]
        public int books_id { get; set; }
        public string status_rental { get; set; }
    }
}
