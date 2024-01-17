using System.Text.Json;
using WeatherAppRemake.Models;

namespace WeatherAppRemake.Services
{
    public class ApiService
    {
        private  HttpClient _httpClient;
        private readonly string _apiKey = "c3a8805a5dce43b5c1f499d450a940a6";
        public LocationResponse locationResponse { get; set; }
        public WeatherResponse weatherResponse { get; set; }
        public int numberOfDays { get; set; }


        public async Task<LocationResponse> GetCityCoords(string city)
        {
            _httpClient = new HttpClient();
            var apiUrl = $"http://api.openweathermap.org/geo/1.0/direct?q={city}&appid={_apiKey}";
            var response = await _httpClient.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                locationResponse = JsonSerializer.Deserialize<LocationResponse>(content);
               return locationResponse;
            }
            else
            {
                return null;
            }
        }

        public async Task<WeatherResponse> GetWeatherAsync(LocationResponse locationResponse)
        {
            _httpClient = new HttpClient();
            double lat = locationResponse.cities[0].lat;
            double lon = locationResponse.cities[0].lon;
            var apiUrl = $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={_apiKey}";
            var response = await _httpClient.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                weatherResponse = JsonSerializer.Deserialize<WeatherResponse>(content);
                return weatherResponse;
            }
            else
            {
                return null;
            }
        }
    }
}
