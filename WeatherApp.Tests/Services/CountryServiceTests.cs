using System.Collections.Generic;
using System.Linq;
using WeatherApp.Models;
using WeatherApp.Services;
using Xunit;

namespace WeatherApp.Tests.Services
{
    public class CountryServiceTests
    {
        [Fact]
        public void GetCountries_ReturnsAllCountriesWithCodeAndName()
        {
            // Arrange
            var service = new CountryService();

            // Act
            var countries = service.GetCountries();

            // Assert
            Assert.NotNull(countries);
            Assert.Equal(4, countries.Count);
            Assert.All(countries, c =>
            {
                Assert.False(string.IsNullOrWhiteSpace(c.Code));
                Assert.False(string.IsNullOrWhiteSpace(c.Name));
                Assert.Empty(c.Cities); // Should not expose cities in this method
            });
        }

        [Theory]
        [InlineData("US", new[] { "New York", "Los Angeles", "Chicago", "Houston", "Miami" })]
        [InlineData("UK", new[] { "London", "Manchester", "Birmingham", "Liverpool", "Edinburgh" })]
        [InlineData("CA", new[] { "Toronto", "Vancouver", "Montreal", "Calgary", "Ottawa" })]
        [InlineData("AU", new[] { "Sydney", "Melbourne", "Brisbane", "Perth", "Adelaide" })]
        public void GetCitiesByCountryCode_ReturnsCorrectCities(string countryCode, string[] expectedCities)
        {
            // Arrange
            var service = new CountryService();

            // Act
            var cities = service.GetCitiesByCountryCode(countryCode);

            // Assert
            Assert.NotNull(cities);
            Assert.Equal(expectedCities.Length, cities.Count);
            foreach (var cityName in expectedCities)
            {
                Assert.Contains(cities, c => c.Name == cityName && c.CountryCode == countryCode);
            }
        }

        [Fact]
        public void GetCitiesByCountryCode_ReturnsEmptyList_ForUnknownCountry()
        {
            // Arrange
            var service = new CountryService();

            // Act
            var cities = service.GetCitiesByCountryCode("ZZ");

            // Assert
            Assert.NotNull(cities);
            Assert.Empty(cities);
        }
    }
}