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
    }
}