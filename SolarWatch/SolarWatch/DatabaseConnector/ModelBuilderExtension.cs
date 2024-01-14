using Microsoft.EntityFrameworkCore;
using SolarWatch.Models;

namespace SolarWatch.DatabaseConnector
{
    public static class ModelBuilderExtension
    {
        public static void ConfigureCity(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>().HasKey(c => c.Id);
            modelBuilder.Entity<SunriseAndSunset>().HasKey(s => s.Id);

            modelBuilder.Entity<City>()
                .HasOne(c => c.SunriseAndSunset)
                .WithOne(s => s.City)
                .HasForeignKey<SunriseAndSunset>(s => s.CityId);
        }
    }
}
