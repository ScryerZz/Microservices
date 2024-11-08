using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Biblioteka.Model
{
    public class Readers
    {
        [Key]
        public int Id { get; set; } 
        public string FirstName { get; set; } 
        public string LastName { get; set; } 
        public string ContactDetails { get; set; }
        public int? RegistrationDate { get; set; }

    }
}
