using WeatherApp.Models;

namespace WeatherApp.Services
{
    public interface ICountryService
    {
        List<Country> GetCountries();
        List<City> GetCitiesByCountryCode(string countryCode);
    }
}
