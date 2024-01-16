using System.Text.Json.Serialization;

namespace SolarWatch.Models
{
    public class SunriseAndSunsetDTO
    {
        public DateTime Sunrise { get; set; }
        public DateTime Sunset { get; set; }

        public SunriseAndSunsetDTO(DateTime sunrise, DateTime sunset)
        {
            Sunrise = sunrise;
            Sunset = sunset;
        }
    }
}
