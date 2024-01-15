using SolarWatch.Models;

namespace SolarWatch.Services
{
    public interface IJsonProcessor
    {
        (DateTime, DateTime) ProcessJsonSunriseAndSunset(string data);
        City ProcessCityFromApi(string data);
    }
}
