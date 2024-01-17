using System.Text.Json;
using WeatherAppRemake.Models;

namespace WeatherAppRemake.Services
{
    public class ApiService
    {
        private  HttpClient _httpClient;
        private readonly string _apiKey = "6dc5bbe531d54037849115258241201";
        public WeatherResponse apiResult { get; set; }
        public int numberOfDays { get; set; }


        public async Task<WeatherResponse> GetWeatherAsync(string city)
        {
            _httpClient = new HttpClient();
            numberOfDays = 5;
            var apiUrl = $"http://api.weatherapi.com/v1/forecast.json?key={_apiKey}&q={city}&days={numberOfDays}&aqi=no&alerts=no";
            var response = await _httpClient.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                apiResult = JsonSerializer.Deserialize<WeatherResponse>(content);
               return apiResult;
            }
            else
            {
                return null;
            }
        }
    }
}
