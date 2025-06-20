# Weather Application

A modern ASP.NET Core MVC web application that allows users to select a country, choose a city, and view current weather information.

## Features

- **Country Selection**: Browse through a list of countries
- **City Selection**: View cities available in the selected country
- **Weather Information**: Get detailed weather data including:
  - Temperature (both Fahrenheit and Celsius)
  - Wind speed and direction
  - Humidity and pressure
  - Visibility and dew point
  - Sky conditions

## Technology Stack

- **Backend**: ASP.NET Core 8.0 MVC
- **Frontend**: Razor Pages with Bootstrap 5 and vanilla JavaScript
- **Testing**: xUnit with Moq for mocking
- **Weather API**: OpenWeatherMap (with fallback to mock data)

## Prerequisites

- .NET 8.0 SDK or later
- Visual Studio 2022 or Visual Studio Code
- (Optional) OpenWeatherMap API key for real weather data

## Setup Instructions

### 1. Clone the Repository
```bash
git clone <your-repository-url>
cd WeatherApp
```

### 2. Restore Dependencies
```bash
dotnet restore
```

### 3. Configure Weather API (Optional)
To use real weather data, get a free API key from [OpenWeatherMap](https://openweathermap.org/api) and update `appsettings.json`:

```json
{
  "WeatherApi": {
    "ApiKey": "your-api-key-here",
    "BaseUrl": "https://api.openweathermap.org/data/2.5"
  }
}
```

**Note**: The application works without an API key by using mock weather data.

### 4. Build the Application
```bash
dotnet build
```

### 5. Run the Application
```bash
dotnet run --project WeatherApp
```

The application will be available at:
- HTTPS: `https://localhost:5001`
- HTTP: `http://localhost:5000`

## Running Tests

### Run All Tests
```bash
dotnet test
```

### Run Tests with Coverage
```bash
dotnet test --collect:"XPlat Code Coverage"
```

### Run Specific Test Project
```bash
dotnet test WeatherApp.Tests
```

## Project Structure

```
WeatherApp/
├── WeatherApp/                     # Main web application
│   ├── Controllers/                # MVC Controllers and API endpoints
│   │   ├── CountriesController.cs  # Countries API endpoints
│   │   ├── WeatherController       # Weather API endpoints
│   │   └── HomeController.cs       # Main page controller
│   ├── Models/                     # Data models and DTOs
│   │   └── WeatherModels.cs        # Weather, Country, and City models
│   ├── Services/                   # Business logic services
│   │   ├── IWeatherService.cs      # Weather service interface
│   │   ├── WeatherService.cs       # Weather service implementation
│   │   ├── ICountryService.cs      # Country service interface
│   │   └── CountryService.cs       # Country service implementation
│   ├── Views/                      # Razor views
│   │   ├── Home/
│   │   │   └── Index.cshtml        # Main page
│   │   └── Shared/
│   │       └── _Layout.cshtml      # Layout template
│   ├── wwwroot/                    # Static files
│   │   └── js/
│   │       └── weather.js          # Frontend JavaScript
│   ├── Program.cs                  # Application startup
│   └── appsettings.json            # Configuration
├── WeatherApp.Tests/               # Unit tests
│   ├── Controllers/                # Controller tests
│   └── Services/                   # Service tests
└── WeatherApp.sln                  # Solution file
```

## API Endpoints

The application exposes the following REST API endpoints:

### GET /api/countries
Returns a list of available countries.

**Response:**
```json
[
  {
    "code": "US",
    "name": "United States"
  },
  {
    "code": "CA", 
    "name": "Canada"
  }
]
```

### GET /api/countries/{countryCode}/cities
Returns cities for the specified country.

**Response:**
```json
[
  {
    "name": "New York",
    "countryCode": "US"
  },
  {
    "name": "Los Angeles",
    "countryCode": "US"
  }
]
```

### GET /api/weather/{cityName}
Returns weather information for the specified city.

**Response:**
```json
{
  "location": "London",
  "country": "GB",
  "timeUtc": "2023-12-01T12:00:00Z",
  "windSpeed": 10.5,
  "windDirection": "SW",
  "visibility": 10.0,
  "skyConditions": "clear sky",
  "temperatureFahrenheit": 68.0,
  "temperatureCelsius": 20.0,
  "dewPoint": 55.0,
  "relativeHumidity": 65.0,
  "pressure": 1013.0
}
```