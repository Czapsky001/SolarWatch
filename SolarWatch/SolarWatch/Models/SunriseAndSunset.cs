using System.Text.Json.Serialization;

namespace SolarWatch.Models
{
    public class SunriseAndSunset
    {
        public Guid Id { get; set; }
        public int CityId { get; set; }
        public DateTime Sunrise { get; set; }
        public DateTime Sunset { get; set; }

        [JsonIgnore]
        public City City { get; set; }
        public SunriseAndSunset(DateTime sunrise, DateTime sunset)
        {
            Sunrise = sunrise;
            Sunset = sunset;
        }
    }
}
