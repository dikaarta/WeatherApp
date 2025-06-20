using WeatherApp.Models;

namespace WeatherApp.Services
{
    public interface IWeatherService
    {
        Task<WeatherResponse> GetWeatherAsync(string cityName);
        double ConvertFahrenheitToCelsius(double fahrenheit);
    }
}
