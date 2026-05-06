using GitGardens.Interface;
using GitGardens.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

/*
     Title: Disclosure of AI Usage in my Assessment.
     • Section: WeatherApi Service & WeatherController.
     • AI Tool: Gemini
     • Purpose/intention : Design of a Weather Service that fetches weather data from an external API and integrates it into the application.
     • Date(s) 05/05/2026.
     • https://chatgpt.com/share/69fb03d0-798c-83ea-8f7b-9ec21b3d39b6
     */

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