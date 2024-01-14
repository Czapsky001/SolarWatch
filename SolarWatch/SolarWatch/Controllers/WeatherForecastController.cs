using Microsoft.AspNetCore.Mvc;
using SolarWatch.Models;
using SolarWatch.Services;

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

        [HttpGet(Name = "SunriseAndSunset")]
        public async Task<ActionResult<SunriseAndSunset>> GetSunriseAndSunset(string city)
        {
            if(!await _dbService.IsExist(city))
            {
                var cityFromApi = await _weatherService.GetCityFromApi(city);
                _dbService.AddCity(cityFromApi);
                return 
            }
            return await _dbService.GetSunriseAndSunsetFromDatabase(city);


        }
    }
}
