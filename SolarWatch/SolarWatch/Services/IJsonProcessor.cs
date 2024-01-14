namespace SolarWatch.Services
{
    public interface IJsonProcessor
    {
        (DateTime, DateTime) ProcessJsonSunriseAndSunset(string data);
    }
}
