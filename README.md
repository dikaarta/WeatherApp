# WeatherApp

A .NET 8 Razor Pages application that provides weather information using the OpenWeatherMap API.

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) (recommended) or any other IDE
- OpenWeatherMap API key (required for weather data)

## Configuration

1. Clone the repository
2. Navigate to the project directory
3. Configure the OpenWeatherMap API key:
   - Open `appsettings.json`
   - Add your OpenWeatherMap API key to the `WeatherApi:ApiKey` section: ```json
 "WeatherApi": {
       "ApiKey": "your-api-key-here",
       "BaseUrl": "https://api.openweathermap.org/data/2.5"
     }
 ```
## Running the Application

### Using Visual Studio 2022
1. Open the solution file in Visual Studio 2022
2. Press __F5__ to run the application in debug mode, or press __Ctrl+F5__ to run without debugging
3. The application will launch in your default browser

### Using Command Line
1. Open a terminal in the project directory
2. Run the following commands:
3. Open a web browser and navigate to:
- https://localhost:5001 (HTTPS)
- http://localhost:5000 (HTTP)

## Features

- Weather information display
- Country and city selection
- Real-time weather updates

## Project Structure

- `Controllers/`: Contains API and MVC controllers
- `Models/`: Data models and DTOs
- `Services/`: Business logic and external API integration
- `Views/`: UI templates
- `wwwroot/`: Static files (CSS, JavaScript, images)