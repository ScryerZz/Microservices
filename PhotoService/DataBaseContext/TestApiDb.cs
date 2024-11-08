using Biblioteka.Model;
using Microsoft.EntityFrameworkCore;

namespace Biblioteka.DataBaseContext
{
    public class TestApiDb : DbContext
    {
        public TestApiDb(DbContextOptions options) : base(options) 
        { 

        }

        public DbSet<Photo> Photos { get; set; }

    }
}
