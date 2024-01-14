using System.Text.Json;

namespace SolarWatch.Services
{
    public class JsonProcessor : IJsonProcessor
    {
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
