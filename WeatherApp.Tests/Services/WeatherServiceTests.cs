using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;
using System.Net;
using WeatherApp.Services;

public class WeatherServiceTests
{
    [Fact]
    public async Task GetWeatherAsync_ReturnsMockData_WhenApiKeyIsMissing()
    {
        // Arrange
        var mockConfig = new ConfigurationBuilder().AddInMemoryCollection().Build();
        var httpClient = new HttpClient();
        var service = new WeatherService(httpClient, mockConfig);

        // Act
        var result = await service.GetWeatherAsync("TestCity");

        // Assert
        Assert.NotNull(result);
        Assert.Contains("TestCity", result.Location);
        Assert.InRange(result.TemperatureFahrenheit, 32, 95);
    }

    [Fact]
    public void ConvertFahrenheitToCelsius_ReturnsCorrectValue()
    {
        // Arrange
        var mockConfig = new ConfigurationBuilder().AddInMemoryCollection().Build();
        var httpClient = new HttpClient();
        var service = new WeatherService(httpClient, mockConfig);

        // Act
        var celsius = service.ConvertFahrenheitToCelsius(68);

        // Assert
        Assert.Equal(20, celsius);
    }

    [Fact]
    public async Task GetWeatherAsync_ReturnsWeatherData_OnValidApiResponse()
    {
        // Arrange
        var apiKey = "dummy";
        var config = new ConfigurationBuilder()
            .AddInMemoryCollection(new[]
            {
                new KeyValuePair<string, string>("OpenWeatherMap:ApiKey", apiKey)
            })
            .Build();

        var responseJson = @"{
            ""name"": ""London"",
            ""sys"": { ""country"": ""GB"" },
            ""wind"": { ""speed"": 5.5, ""deg"": 180 },
            ""visibility"": 10000,
            ""weather"": [ { ""description"": ""clear sky"" } ],
            ""main"": { ""temp"": 68, ""humidity"": 50, ""pressure"": 1012 }
        }";

        var handlerMock = new Mock<HttpMessageHandler>();
        handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(responseJson),
            });

        var httpClient = new HttpClient(handlerMock.Object);
        var service = new WeatherService(httpClient, config);

        // Act
        var result = await service.GetWeatherAsync("London");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("London, GB", result.Location);
        Assert.Equal("clear sky", result.SkyConditions);
        Assert.Equal(68, result.TemperatureFahrenheit);
        Assert.Equal(20, result.TemperatureCelsius);
        Assert.Equal(5.5, result.Wind.Speed);
        Assert.Equal(0, result.Wind.Direction);
        Assert.Equal(10, result.Visibility);
        Assert.Equal(50, result.RelativeHumidity);
        Assert.Equal(1012, result.Pressure);
    }
}