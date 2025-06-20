using Microsoft.AspNetCore.Mvc;
using WeatherApp.Services;

namespace WeatherApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherService _weatherService;

        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet("{cityName}")]
        public async Task<IActionResult> GetWeather(string cityName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(cityName))
                {
                    return BadRequest(new { message = "City name is required" });
                }

                var weather = await _weatherService.GetWeatherAsync(cityName);
                return Ok(weather);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving weather data", error = ex.Message });
            }
        }
    }
}