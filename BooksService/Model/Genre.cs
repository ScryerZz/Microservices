using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Biblioteka.Model
{
    public class Genre
    {
        [Key]
        public int Id { get; set; } 
        public string Name { get; set; }

    }
}

