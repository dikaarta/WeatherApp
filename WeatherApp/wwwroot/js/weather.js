class WeatherApp {
    constructor() {
        this.countrySelect = document.getElementById('countrySelect');
        this.citySelect = document.getElementById('citySelect');
        this.weatherResult = document.getElementById('weatherResult');
        this.loadingSpinner = document.getElementById('loadingSpinner');
        this.errorMessage = document.getElementById('errorMessage');

        this.initializeEventListeners();
        this.loadCountries();
    }

    initializeEventListeners() {
        this.countrySelect.addEventListener('change', () => {
            this.onCountryChange();
        });

        this.citySelect.addEventListener('change', () => {
            this.onCityChange();
        });
    }

    async loadCountries() {
        try {
            this.showLoading();
            const response = await fetch('/api/countries');

            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }

            const countries = await response.json();
            this.populateCountryDropdown(countries);

        } catch (error) {
            this.showError('Failed to load countries: ' + error.message);
        } finally {
            this.hideLoading();
        }
    }

    populateCountryDropdown(countries) {
        this.countrySelect.innerHTML = '<option value="">Select a country</option>';

        countries.forEach(country => {
            const option = document.createElement('option');
            option.value = country.code;
            option.textContent = country.name;
            this.countrySelect.appendChild(option);
        });

        this.countrySelect.disabled = false;
    }

    async onCountryChange() {
        const countryCode = this.countrySelect.value;

        // Reset city dropdown and weather result
        this.citySelect.innerHTML = '<option value="">Select a city</option>';
        this.citySelect.disabled = true;
        this.hideWeatherResult();

        if (!countryCode) {
            return;
        }

        try {
            this.showLoading();
            const response = await fetch(`/api/countries/${countryCode}/cities`);

            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }

            const cities = await response.json();
            this.populateCityDropdown(cities);

        } catch (error) {
            this.showError('Failed to load cities: ' + error.message);
        } finally {
            this.hideLoading();
        }
    }

    populateCityDropdown(cities) {
        this.citySelect.innerHTML = '<option value="">Select a city</option>';

        cities.forEach(city => {
            const option = document.createElement('option');
            option.value = city.name;
            option.textContent = city.name;
            this.citySelect.appendChild(option);
        });

        this.citySelect.disabled = false;
    }

    async onCityChange() {
        const cityName = this.citySelect.value;

        this.hideWeatherResult();

        if (!cityName) {
            return;
        }

        try {
            this.showLoading();
            const response = await fetch(`/api/weather/${encodeURIComponent(cityName)}`);

            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }

            const weather = await response.json();
            this.displayWeatherResult(weather);

        } catch (error) {
            this.showError('Failed to load weather data: ' + error.message);
        } finally {
            this.hideLoading();
        }
    }

    displayWeatherResult(weather) {
        // Update weather information
        document.getElementById('weatherLocation').textContent = `${weather.location}, ${weather.country}`;
        document.getElementById('weatherTime').textContent = `Updated: ${new Date(weather.timeUtc).toLocaleString()} UTC`;

        document.getElementById('tempF').textContent = Math.round(weather.temperatureFahrenheit);
        document.getElementById('tempC').textContent = Math.round(weather.temperatureCelsius);

        document.getElementById('skyConditions').textContent = weather.skyConditions;
        document.getElementById('windSpeed').textContent = Math.round(weather.windSpeed);
        document.getElementById('windDirection').textContent = weather.windDirection;

        document.getElementById('humidity').textContent = `${Math.round(weather.relativeHumidity)}%`;
        document.getElementById('pressure').textContent = `${Math.round(weather.pressure)} hPa`;
        document.getElementById('visibility').textContent = `${Math.round(weather.visibility)} km`;
        document.getElementById('dewPoint').textContent = `${Math.round(weather.dewPoint)}°F`;

        this.showWeatherResult();
    }

    showLoading() {
        this.loadingSpinner.style.display = 'block';
        this.hideError();
    }

    hideLoading() {
        this.loadingSpinner.style.display = 'none';
    }

    showWeatherResult() {
        this.weatherResult.style.display = 'block';
        this.hideError();
    }

    hideWeatherResult() {
        this.weatherResult.style.display = 'none';
    }

    showError(message) {
        this.errorMessage.textContent = message;
        this.errorMessage.style.display = 'block';
        this.hideWeatherResult();
    }

    hideError() {
        this.errorMessage.style.display = 'none';
    }
}

// Initialize the app when the page loads
document.addEventListener('DOMContentLoaded', () => {
    new WeatherApp();
});