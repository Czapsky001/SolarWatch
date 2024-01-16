using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SolarWatch.DatabaseConnector;
using SolarWatch.Models;

namespace SolarWatch.Services.DbService
{
    public class DbService : IDbService
    {
        private readonly DatabaseContext _dbContext;
        private readonly ILogger<DbService> logger;

        public DbService(DatabaseContext db, ILogger<DbService> logger)
        {
            _dbContext = db;
            this.logger = logger;
        }

        public async Task<bool> IsExist(string city)
        {
            try
            {
                var cityInDatabase = await _dbContext.Cities.Include(c => c.SunriseAndSunset).AnyAsync(e => e.Name == city);

                if (cityInDatabase)
                {
                    var cityEntity = await _dbContext.Cities.Include(c => c.SunriseAndSunset).FirstOrDefaultAsync(e => e.Name == city);
                    if (cityEntity.SunriseAndSunset == null)
                    {
                        return false;
                    }

                    DateTime sunriseData = cityEntity.SunriseAndSunset.Sunrise;

                    bool isSameDay = sunriseData.Day == DateTime.Now.Day;

                    return isSameDay;
                }

                return cityInDatabase;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return false;
            }
        }
        public async Task<bool> AddCity(City city)
        {
            try
            {
                var existingCity = await _dbContext.Cities
                    .Include(c => c.SunriseAndSunset)
                    .FirstOrDefaultAsync(e => e.Name == city.Name);

                if (existingCity != null)
                {
                    existingCity.SunriseAndSunset = city.SunriseAndSunset;
                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    await _dbContext.Cities.AddAsync(city);
                    await _dbContext.SaveChangesAsync();
                }

                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return false;
            }
        }




        public async Task<bool> RemoveCity(City city)
        {
            _dbContext.Cities.Remove(city);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<SunriseAndSunset> GetSunriseAndSunsetFromDatabase(string city)
        {
            var data = await _dbContext.Cities.FirstOrDefaultAsync(e => e.Name == city);
            return data.SunriseAndSunset;
        }
    }
}