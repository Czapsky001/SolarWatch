using SolarWatch.Models;

namespace SolarWatch.Services.WeatherService
{
    public interface IWeatherService
    {
        Task<SunriseAndSunset> GetSunriseAndSunsetAsync(double lat, double lon, DateOnly date);
        Task<City> GetCity(string city);
    }
}