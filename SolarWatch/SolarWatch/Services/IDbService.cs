using SolarWatch.Models;

namespace SolarWatch.Services
{
    public interface IDbService
    {
        Task<bool> IsExist(string city);
        Task<bool> AddCity(string city);
        Task<bool> RemoveCity(string city);
        Task<SunriseAndSunset> GetSunriseAndSunsetFromDatabase(string city);
    }
}
