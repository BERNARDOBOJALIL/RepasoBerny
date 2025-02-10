using Microsoft.EntityFrameworkCore;
using RepasoBerny.Shared.Entities;

namespace RepasoBerny.Web.Data
{
    public class DataContext: DbContext
    {
        public DbSet <City> Cities { get; set; }
        public DataContext(DbContextOptions <DataContext> options): base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<City>().HasIndex(x => x.Name).IsUnique();
        }
    }
}
