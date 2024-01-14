using Microsoft.EntityFrameworkCore;
using SolarWatch.Models;

namespace SolarWatch.DatabaseConnector
{
    public class DatabaseContext : DbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<SunriseAndSunset> SunriseAndSunset { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
