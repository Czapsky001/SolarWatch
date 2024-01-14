namespace SolarWatch.Models
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public SunriseAndSunset SunriseAndSunset { get; set; }

        public City(string name, string state, string country, double longitude, double latitude)
        {
            Name = name;
            State = state;
            Country = country;
            Longitude = longitude;
            Latitude = latitude;
        }

    }

}
