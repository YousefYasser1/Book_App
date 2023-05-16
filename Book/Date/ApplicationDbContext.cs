using Microsoft.EntityFrameworkCore;
using Book.Models;

namespace Book.Date
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {           
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<BooK> BooKs { get; set; }
    }
}
