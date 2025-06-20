namespace WeatherApp.Models
{
    public class WeatherResponse
    {
        public string Location { get; set; } = string.Empty;
        public DateTime Time { get; set; }
        public WindInfo Wind { get; set; } = new();
        public double Visibility { get; set; }
        public string SkyConditions { get; set; } = string.Empty;
        public double TemperatureFahrenheit { get; set; }
        public double TemperatureCelsius { get; set; }
        public double DewPoint { get; set; }
        public double RelativeHumidity { get; set; }
        public double Pressure { get; set; }
    }

    public class WindInfo
    {
        public double Speed { get; set; }
        public int Direction { get; set; }
    }
}