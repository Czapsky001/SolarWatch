using SolarWatch.Models;
using System.Text.Json;

namespace SolarWatch.Services
{
    public class JsonProcessor : IJsonProcessor
    {
        public City ProcessCityFromApi(string data)
        {
            JsonDocument result = JsonDocument.Parse(data);
            JsonElement main = result.RootElement;
            JsonElement cord = main.GetProperty("coord");
            JsonElement sys = main.GetProperty("sys");
            double lat = cord.GetProperty("lat").GetDouble();
            double lon = cord.GetProperty("lon").GetDouble();
            string name = main.GetProperty("name").GetString();
            string country = sys.GetProperty("country").GetString();


            return new City(name, country, lon, lat);
        }

        public (DateTime, DateTime) ProcessJsonSunriseAndSunset(string data)
        {
            JsonDocument jsonDocument = JsonDocument.Parse(data);
            JsonElement result = jsonDocument.RootElement.GetProperty("results");
            string sunriseString = result.GetProperty("sunrise").GetString();
            string sunsetString = result.GetProperty("sunset").GetString();
            DateTime sunrise = DateTime.Parse(sunriseString);
            DateTime sunset = DateTime.Parse(sunsetString);

            return (sunrise, sunset);
        }
    }
}
