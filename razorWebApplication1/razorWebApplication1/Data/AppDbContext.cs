using Microsoft.EntityFrameworkCore;
using razorWebApplication1.Models;
using razorWebApplication1.Pages;

namespace razorWebApplication1.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Product>(entity =>
        //    {
        //        entity.ToTable("PRODUCT");
        //        entity.Property(e => e.Id).HasColumnName("ID");
        //        entity.Property(e => e.Name).HasColumnName("NAME");
        //        entity.Property(e => e.Price).HasColumnName("PRICE");
        //    });
        //}

    }
}
