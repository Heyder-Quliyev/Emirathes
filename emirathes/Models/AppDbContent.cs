using Microsoft.EntityFrameworkCore;

namespace emirathes.Models
{
    public class AppDbContent:DbContext
    {
        public AppDbContent(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Tickts> Ticktes { get; set; }
        public DbSet<Users> Users { get; set; }
    }
}
