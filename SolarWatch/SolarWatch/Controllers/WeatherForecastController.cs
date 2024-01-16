using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SolarWatch.Models;
using SolarWatch.Services.DbService;
using SolarWatch.Services.WeatherService;

namespace SolarWatch.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherService _weatherService;
        private readonly IDbService _dbService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherService weatherService, IDbService dbService)
        {
            _logger = logger;
            _weatherService = weatherService;
            _dbService = dbService;
        }

        [HttpGet(Name = "SunriseAndSunset"), Authorize]
        public async Task<ActionResult<SunriseAndSunset>> GetSunriseAndSunset(string cityName)
        {
            if (!await _dbService.IsExist(cityName))
            {
                var cityFromApi = await _weatherService.GetCity(cityName);
                await _dbService.AddCity(cityFromApi);

                var sunrise = cityFromApi.SunriseAndSunset.Sunrise;
                var sunset = cityFromApi.SunriseAndSunset.Sunset;
                return new SunriseAndSunset(sunrise, sunset);
            }
            return await _dbService.GetSunriseAndSunsetFromDatabase(cityName);
        }
    }
}