namespace WeatherApp.Models
{
    public class WeatherApiOptions
    {
        public string ApiKey { get; set; } = string.Empty;
        public string BaseUrl { get; set; } = "https://api.openweathermap.org/data/2.5";
    }
}
