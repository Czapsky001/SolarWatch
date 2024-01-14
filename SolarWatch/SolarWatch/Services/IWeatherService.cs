using SolarWatch.Models;

namespace SolarWatch.Services
{
    public interface IWeatherService
    {
        Task<(DateTime, DateTime)> GetSunriseAndSunsetAsync(double lat, double lon, DateOnly date);

        Task<(double, double)> GetLatAndLonFromCityName(string city);

        Task<SunriseAndSunset> GetCityFromApi(string city);
    }
}
