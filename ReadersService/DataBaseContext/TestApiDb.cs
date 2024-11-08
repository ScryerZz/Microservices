using Biblioteka.Model;
using Microsoft.EntityFrameworkCore;

namespace Biblioteka.DataBaseContext
{
    public class TestApiDb : DbContext
    {
        public TestApiDb(DbContextOptions options) : base(options) 
        { 

        }

        public DbSet<Books> Books { get; set; }
        public DbSet<Readers> Readers { get; set; }
        public DbSet<Genre> Genres { get; set; }
    }
}
