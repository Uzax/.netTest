using Mango.Services.LoggingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.LoggingAPI.Data
{

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Logs> Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            
            modelBuilder.Entity<Logs>()
                .HasKey(c => c.Id); // Define the primary key
            
            
            modelBuilder.Entity<Logs>()
                .Property(f => f.Id)
                .ValueGeneratedOnAdd();
        }
    }
}