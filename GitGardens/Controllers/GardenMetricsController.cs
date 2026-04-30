using GitGardens.Interface;
using GitGardens.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GitGardens.Controllers
{

    /*
       Title: ASP.NET Core MVC CRUD - NET 6 MVC CRUD  
       Operations Using Entity Framework Core and SQL Server  
       Author: Sameer Saini  
       Date: 2 years ago
       Date accessed: 27/09/2025
       Code version : 1  
       Availability: https://youtu.be/2Cp8Ti_f9Gk?feature=shared
    */

    /*
       Title: ASP.NET 8 MVC Tutorial for Beginners - C# web development made easy  
       Operations Using Entity Framework Core and SQL Server  
       Author: tutorialsEU  
       Date: 1 years ago
       Date accessed: 27/09/2025
       Code version : 1  
       Availability: https://youtu.be/xuFdrXqpPB0?feature=shared
    */

    public class GardenMetricsController : Controller
    {
        private readonly IGardenMetricsService _metricsService;

        public GardenMetricsController(IGardenMetricsService metricsService)
        {
            _metricsService = metricsService;
        }


        // GET: GardenMetrics/PostMetric?gardenID=5
        public async Task<IActionResult> PostMetric(int gardenID)
        {
            // Fetch the last recorded data for this specific garden
            var latest = await _metricsService.GetLatestMetricsAsync(gardenID);

            // Populate the ViewModel with existing data or default values if none exist
            var model = new AddGardenMetric
            {
                GardenID = gardenID,
                Moisture = latest?.Moisture ?? 50,
                PH = latest?.PH ?? 6.5m,
                Temperature = latest?.Temperature ?? 22,
                Humidity = latest?.Humidity ?? 50,
                Sunlight = latest?.Sunlight ?? 50,
                Nitrogen = latest?.Nitrogen ?? 50
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostMetric(AddGardenMetric model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userIDClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIDClaim == null)
            {
                return Unauthorized();
            }

            int userID = int.Parse(userIDClaim.Value);

            var success = await _metricsService.AddMetricAsync(
                model.GardenID,
                userID,
                model.Moisture,
                model.PH,
                model.Temperature,
                model.Humidity,
                model.Sunlight,
                model.Nitrogen
            );

            if (!success)
            {
                return Unauthorized();
            }

            return RedirectToAction("GardenList", "Garden");

        }

    }
}
