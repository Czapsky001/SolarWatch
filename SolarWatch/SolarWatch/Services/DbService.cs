using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SolarWatch.DatabaseConnector;
using SolarWatch.Models;

namespace SolarWatch.Services
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
                return await _dbContext.Cities.AnyAsync(e => e.Name == city);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return false;

            }

        }
        public async Task<bool> AddCity(City city)
        {
            await _dbContext.Cities.AddAsync(city);
            await _dbContext.SaveChangesAsync();
            return true;
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
            throw new NotImplementedException();
        }
    }
}
