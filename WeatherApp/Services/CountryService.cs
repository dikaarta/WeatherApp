using WeatherApp.Models;

namespace WeatherApp.Services
{
    public class CountryService : ICountryService
    {
        private readonly List<Country> _countries;

        public CountryService()
        {
            _countries = InitializeCountries();
        }

        public List<Country> GetCountries()
        {
            return _countries.Select(c => new Country
            {
                Code = c.Code,
                Name = c.Name
            }).ToList();
        }

        public List<City> GetCitiesByCountryCode(string countryCode)
        {
            var country = _countries.FirstOrDefault(c => c.Code.Equals(countryCode, StringComparison.OrdinalIgnoreCase));
            return country?.Cities ?? new List<City>();
        }

        private List<Country> InitializeCountries()
        {
            return new List<Country>
            {
                new Country
                {
                    Code = "US",
                    Name = "United States",
                    Cities = new List<City>
                    {
                        new City { Name = "New York", CountryCode = "US" },
                        new City { Name = "Los Angeles", CountryCode = "US" },
                        new City { Name = "Chicago", CountryCode = "US" },
                        new City { Name = "Houston", CountryCode = "US" },
                        new City { Name = "Miami", CountryCode = "US" }
                    }
                },
                new Country
                {
                    Code = "UK",
                    Name = "United Kingdom",
                    Cities = new List<City>
                    {
                        new City { Name = "London", CountryCode = "UK" },
                        new City { Name = "Manchester", CountryCode = "UK" },
                        new City { Name = "Birmingham", CountryCode = "UK" },
                        new City { Name = "Liverpool", CountryCode = "UK" },
                        new City { Name = "Edinburgh", CountryCode = "UK" }
                    }
                },
                new Country
                {
                    Code = "CA",
                    Name = "Canada",
                    Cities = new List<City>
                    {
                        new City { Name = "Toronto", CountryCode = "CA" },
                        new City { Name = "Vancouver", CountryCode = "CA" },
                        new City { Name = "Montreal", CountryCode = "CA" },
                        new City { Name = "Calgary", CountryCode = "CA" },
                        new City { Name = "Ottawa", CountryCode = "CA" }
                    }
                },
                new Country
                {
                    Code = "AU",
                    Name = "Australia",
                    Cities = new List<City>
                    {
                        new City { Name = "Sydney", CountryCode = "AU" },
                        new City { Name = "Melbourne", CountryCode = "AU" },
                        new City { Name = "Brisbane", CountryCode = "AU" },
                        new City { Name = "Perth", CountryCode = "AU" },
                        new City { Name = "Adelaide", CountryCode = "AU" }
                    }
                }
            };
        }
    }
}
