using ChristmasOnlineWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace ChristmasOnlineWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Category>()
                .HasIndex(c => c.Name)
                .IsUnique();
        }
        public DbSet<Category> Categories { get; set; }
    }
}
