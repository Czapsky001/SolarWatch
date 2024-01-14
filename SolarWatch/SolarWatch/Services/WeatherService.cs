

using SolarWatch.ApiConnect;
using SolarWatch.Models;
using System.Text.Json;

namespace SolarWatch.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IWebClient _webClient;
        private readonly ILogger<WeatherService> _logger;
        private readonly IJsonProcessor _jsonProcessor;

        public WeatherService(IWebClient webClient, ILogger<WeatherService> logger, IJsonProcessor jsonProcessor)
        {
            _webClient = webClient;
            _logger = logger;
            _jsonProcessor = jsonProcessor;
            
        }

        public async Task<SunriseAndSunset> GetCityFromApi(string city)
        {
            var data = await GetLatAndLonFromCityName(city);
            var lat = data.Item1;
            var lon = data.Item2;
            var sunriseAndSunset = await GetSunriseAndSunsetAsync(lat, lon, DateOnly.FromDateTime(DateTime.Now));
            var sunrise = sunriseAndSunset.Item1;
            var sunset = sunriseAndSunset.Item2;

            return new SunriseAndSunset(sunrise, sunset);
        }

        public async Task<(double, double)> GetLatAndLonFromCityName(string city)
        {
            var apiKey = "9582407a79f5d75534625aa2be480c30";
            var url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}";
            var data = await _webClient.DownloadStringAsync(url);
            JsonDocument result = JsonDocument.Parse(data);
            JsonElement coords = result.RootElement.GetProperty("coord");
            double lat = coords.GetProperty("lat").GetDouble();
            double lon = coords.GetProperty("lon").GetDouble();



            return (lat, lon);
        }

        public async Task<(DateTime, DateTime)> GetSunriseAndSunsetAsync(double lat, double lon, DateOnly date)
        {
            var urlApi = $"https://api.sunrise-sunset.org/json?lat={lat}&lng={lon}&date={date}";
            var data = await _webClient.DownloadStringAsync(urlApi);
            return _jsonProcessor.ProcessJsonSunriseAndSunset(data);
        }
    }
}
