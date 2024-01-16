using SolarWatch.ApiConnect;
using SolarWatch.Models;
using System.Text.Json;

namespace SolarWatch.Services.WeatherService
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

        /*        public async Task<City> GetCityFromApi(string city)
                {
                    var data = await GetCity(city);
                    var lat = data.Item1;
                    var lon = data.Item2;
                    var sunriseAndSunset = await GetSunriseAndSunsetAsync(lat, lon, DateOnly.FromDateTime(DateTime.Now));
                    var sunrise = sunriseAndSunset.Item1;
                    var sunset = sunriseAndSunset.Item2;

                    throw new Exception();
                }*/

        public async Task<City> GetCity(string city)
        {
            var apiKey = "9582407a79f5d75534625aa2be480c30";
            var url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}";
            var data = await _webClient.DownloadStringAsync(url);
            var result = _jsonProcessor.ProcessCityFromApi(data);
            var date = DateOnly.FromDateTime(DateTime.Now);
            var sunsetAndSunrise = await GetSunriseAndSunsetAsync(result.Latitude, result.Longitude, date);
            result.SunriseAndSunset = sunsetAndSunrise;
            sunsetAndSunrise.CityId = result.Id;
            sunsetAndSunrise.City = result;


            return result;
        }

        public async Task<SunriseAndSunset> GetSunriseAndSunsetAsync(double lat, double lon, DateOnly date)
        {
            var urlApi = $"https://api.sunrise-sunset.org/json?lat={lat}&lng={lon}&date={date}";
            var data = await _webClient.DownloadStringAsync(urlApi);
            var sunriseAndSunset = _jsonProcessor.ProcessJsonSunriseAndSunset(data);

            return new SunriseAndSunset(sunriseAndSunset.Item1, sunriseAndSunset.Item2);
        }
    }
}