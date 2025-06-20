namespace WeatherApp.Models
{
    public class OpenWeatherMapResponse
    {
        public MainInfo Main { get; set; } = new();
        public List<WeatherInfo> Weather { get; set; } = new();
        public WindInfo Wind { get; set; } = new();
        public long Visibility { get; set; }
        public string Name { get; set; } = string.Empty;
        public SysInfo Sys { get; set; } = new();
    }

    public class MainInfo
    {
        public double Temp { get; set; }
        public double Humidity { get; set; }
        public double Pressure { get; set; }
    }

    public class WeatherInfo
    {
        public string Main { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

    public class SysInfo
    {
        public string Country { get; set; } = string.Empty;
    }
}