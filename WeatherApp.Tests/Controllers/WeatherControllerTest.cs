using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WeatherApp.Controllers;
using WeatherApp.Models;
using WeatherApp.Services;
using Xunit;

namespace WeatherApp.Tests.Controllers
{
    public class WeatherControllerTest
    {
        [Fact]
        public async Task GetWeather_ReturnsOk_WithWeatherData()
        {
            // Arrange
            var mockService = new Mock<IWeatherService>();
            var expectedWeather = new WeatherResponse { Location = "Test City" };
            mockService.Setup(s => s.GetWeatherAsync("Test City"))
                .ReturnsAsync(expectedWeather);
            var controller = new WeatherController(mockService.Object);

            // Act
            var result = await controller.GetWeather("Test City");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expectedWeather, okResult.Value);
        }

        [Fact]
        public async Task GetWeather_ReturnsBadRequest_WhenCityNameMissing()
        {
            // Arrange
            var mockService = new Mock<IWeatherService>();
            var controller = new WeatherController(mockService.Object);

            // Act
            var result = await controller.GetWeather(null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task GetWeather_Returns500_OnException()
        {
            // Arrange
            var mockService = new Mock<IWeatherService>();
            mockService.Setup(s => s.GetWeatherAsync(It.IsAny<string>()))
                .ThrowsAsync(new Exception("fail"));
            var controller = new WeatherController(mockService.Object);

            // Act
            var result = await controller.GetWeather("Test City");

            // Assert
            var statusResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusResult.StatusCode);
        }
    }
}