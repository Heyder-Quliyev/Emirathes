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
        public DbSet<Passengers> Passengers { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Products> Products { get; set; }
        //public DbSet<Order> Orders { get; set; }

    }
}
