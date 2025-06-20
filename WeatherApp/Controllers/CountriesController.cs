using Microsoft.AspNetCore.Mvc;
using WeatherApp.Services;

namespace WeatherApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountriesController : ControllerBase
    {
        private readonly ICountryService _countryService;

        public CountriesController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpGet]
        public IActionResult GetCountries()
        {
            try
            {
                var countries = _countryService.GetCountries();
                return Ok(countries);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving countries", error = ex.Message });
            }
        }

        [HttpGet("{countryCode}/cities")]
        public IActionResult GetCitiesByCountry(string countryCode)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(countryCode))
                {
                    return BadRequest(new { message = "Country code is required" });
                }

                var cities = _countryService.GetCitiesByCountryCode(countryCode);
                return Ok(cities);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving cities", error = ex.Message });
            }
        }
    }
}