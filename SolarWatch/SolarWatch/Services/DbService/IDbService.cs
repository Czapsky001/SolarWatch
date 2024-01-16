using SolarWatch.Models;

namespace SolarWatch.Services.DbService
{
    public interface IDbService
    {
        Task<bool> IsExist(string city);
        Task<bool> AddCity(City city);
        Task<bool> RemoveCity(City city);
        Task<SunriseAndSunset> GetSunriseAndSunsetFromDatabase(string city);
    }
}
