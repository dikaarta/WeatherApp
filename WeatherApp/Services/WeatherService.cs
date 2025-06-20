using WeatherApp.Models;
using System.Text.Json;

namespace WeatherApp.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly string? _apiKey;

        public WeatherService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _apiKey = _configuration["OpenWeatherMap:ApiKey"];
        }

        public async Task<WeatherResponse> GetWeatherAsync(string cityName)
        {
            // If no API key is configured, return mock data
            if (string.IsNullOrEmpty(_apiKey))
            {
                return GetMockWeatherData(cityName);
            }

            try
            {
                var url = $"https://api.openweathermap.org/data/2.5/weather?q={Uri.EscapeDataString(cityName)}&appid={_apiKey}&units=imperial";
                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return GetMockWeatherData(cityName);
                }

                var json = await response.Content.ReadAsStringAsync();
                var weatherData = JsonSerializer.Deserialize<OpenWeatherMapResponse>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (weatherData == null)
                {
                    return GetMockWeatherData(cityName);
                }

                return new WeatherResponse
                {
                    Location = $"{weatherData.Name}, {weatherData.Sys.Country}",
                    Time = DateTime.UtcNow,
                    Wind = new WindInfo
                    {
                        Speed = weatherData.Wind.Speed,
                        Direction = weatherData.Wind.Direction
                    },
                    Visibility = weatherData.Visibility / 1000.0, // Convert to km
                    SkyConditions = weatherData.Weather.FirstOrDefault()?.Description ?? "clear",
                    TemperatureFahrenheit = weatherData.Main.Temp,
                    TemperatureCelsius = ConvertFahrenheitToCelsius(weatherData.Main.Temp),
                    DewPoint = CalculateDewPoint(weatherData.Main.Temp, weatherData.Main.Humidity),
                    RelativeHumidity = weatherData.Main.Humidity,
                    Pressure = weatherData.Main.Pressure
                };
            }
            catch
            {
                return GetMockWeatherData(cityName);
            }
        }

        public double ConvertFahrenheitToCelsius(double fahrenheit)
        {
            return Math.Round((fahrenheit - 32) * 5.0 / 9.0, 2);
        }

        private WeatherResponse GetMockWeatherData(string cityName)
        {
            var random = new Random();
            var tempF = random.Next(32, 95); // Random temp between 32-95°F

            return new WeatherResponse
            {
                Location = $"{cityName}, Mock Country",
                Time = DateTime.UtcNow,
                Wind = new WindInfo
                {
                    Speed = Math.Round(random.NextDouble() * 20, 1),
                    Direction = random.Next(0, 360)
                },
                Visibility = Math.Round(random.NextDouble() * 10 + 5, 1),
                SkyConditions = new[] { "clear", "cloudy", "partly cloudy", "overcast" }[random.Next(4)],
                TemperatureFahrenheit = tempF,
                TemperatureCelsius = ConvertFahrenheitToCelsius(tempF),
                DewPoint = Math.Round((double)(tempF - random.Next(10, 30)), 1), // Explicit cast to double
                RelativeHumidity = random.Next(30, 90),
                Pressure = Math.Round(random.NextDouble() * 50 + 1000, 1)
            };
        }

        private double CalculateDewPoint(double tempF, double humidity)
        {
            var tempC = ConvertFahrenheitToCelsius(tempF);
            var dewPointC = tempC - ((100 - humidity) / 5.0);
            return Math.Round(dewPointC * 9.0 / 5.0 + 32, 1); // Convert back to Fahrenheit
        }
    }
}