using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RepasoBerny.Shared.Entities;

namespace RepasoBerny.Web.Data
{
    public class DataContext: IdentityDbContext<User>
    {
        public DbSet <City> Cities { get; set; }
        public DbSet<Product> Products { get; set; }
        public DataContext(DbContextOptions <DataContext> options): base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<City>().HasIndex(x => x.Name).IsUnique();
            modelBuilder.Entity<Product>().HasIndex(x => x.Name).IsUnique();
        }
    }
}
