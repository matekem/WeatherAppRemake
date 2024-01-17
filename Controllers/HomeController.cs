using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WeatherAppRemake.Models;
using WeatherAppRemake.Services;

namespace WeatherAppRemake.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private HttpClient _httpClient = new();
        private ApiService _weatherService = new();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> Index(string city)
        {
            city = Request.Form["city"];
            LocationResponse cityName = await _weatherService.GetCityCoords(city);
            WeatherResponse weatherData = await _weatherService.GetWeatherAsync(cityName);

            return View(weatherData);
        }
    }
}
