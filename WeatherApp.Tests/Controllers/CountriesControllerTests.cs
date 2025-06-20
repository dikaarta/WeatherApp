using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WeatherApp.Controllers;
using WeatherApp.Models;
using WeatherApp.Services;
using Xunit;

namespace WeatherApp.Tests.Controllers
{
    public class CountriesControllerTests
    {
        [Fact]
        public void GetCountries_ReturnsOkWithCountries()
        {
            // Arrange
            var mockService = new Mock<ICountryService>();
            var expectedCountries = new List<Country>
            {
                new Country { Code = "US", Name = "United States" }
            };
            mockService.Setup(s => s.GetCountries()).Returns(expectedCountries);
            var controller = new CountriesController(mockService.Object);

            // Act
            var result = controller.GetCountries();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expectedCountries, okResult.Value);
        }

        [Fact]
        public void GetCountries_Returns500OnException()
        {
            // Arrange
            var mockService = new Mock<ICountryService>();
            mockService.Setup(s => s.GetCountries()).Throws(new Exception("fail"));
            var controller = new CountriesController(mockService.Object);

            // Act
            var result = controller.GetCountries();

            // Assert
            var statusResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusResult.StatusCode);
        }

        [Fact]
        public void GetCitiesByCountry_ReturnsOkWithCities()
        {
            // Arrange
            var mockService = new Mock<ICountryService>();
            var expectedCities = new List<City>
            {
                new City { Name = "New York", CountryCode = "US" }
            };
            mockService.Setup(s => s.GetCitiesByCountryCode("US")).Returns(expectedCities);
            var controller = new CountriesController(mockService.Object);

            // Act
            var result = controller.GetCitiesByCountry("US");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expectedCities, okResult.Value);
        }

        [Fact]
        public void GetCitiesByCountry_ReturnsBadRequest_WhenCountryCodeMissing()
        {
            // Arrange
            var mockService = new Mock<ICountryService>();
            var controller = new CountriesController(mockService.Object);

            // Act
            var result = controller.GetCitiesByCountry(null);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void GetCitiesByCountry_Returns500OnException()
        {
            // Arrange
            var mockService = new Mock<ICountryService>();
            mockService.Setup(s => s.GetCitiesByCountryCode(It.IsAny<string>())).Throws(new Exception("fail"));
            var controller = new CountriesController(mockService.Object);

            // Act
            var result = controller.GetCitiesByCountry("US");

            // Assert
            var statusResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusResult.StatusCode);
        }
    }
}