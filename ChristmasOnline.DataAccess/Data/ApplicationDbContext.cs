using ChristmasOnline.Models;
using Microsoft.EntityFrameworkCore;

namespace ChristmasOnline.DataAccess
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

            builder.Entity<Material>()
                .HasIndex(m => m.Name)
                .IsUnique();

            builder.Entity<Product>()
                .HasIndex(p => p.Barcode)
                .IsUnique();
        }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Material> Materials { get; set; }

        public DbSet<Product> Products { get; set; }
    }
}
