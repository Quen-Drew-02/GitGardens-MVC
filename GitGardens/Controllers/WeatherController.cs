using GitGardens.Interface;
using GitGardens.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GitGardens.Controllers
{
    [Authorize]
    public class WeatherController : Controller
    {
        private readonly IWeatherApiService _weatherService;

        public WeatherController(IWeatherApiService weatherService)
        {
            _weatherService = weatherService;
        }

        // GET: /Weather/Index
        // Default loads with a default city, user can search for any city
        public async Task<IActionResult> Index(string city = "Durban")
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                city = "Durban";
            }

            var weather = await _weatherService.GetWeatherAsync(city);

            if (weather == null)
            {
                TempData["WeatherError"] = $"Could not find weather for '{city}'. Please try another city.";
                return View(new ExternalWeatherResponse());
            }

            ViewBag.City = city;
            return View(weather);
        }
    }
}