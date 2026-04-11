<<<<<<< Updated upstream
﻿using GitGardens.Interface;
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


        public IActionResult PostMetric(int gardenID)
        {
            var model = new AddGardenMetric
            {
                GardenID = gardenID
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

=======
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GitGardens.Data;
using GitGardens.Models;
using GitGardens.Services;

namespace GitGardens.Controllers
{
    public class GardenMetricsController : Controller
    {
        private readonly GitGardensDBContext _context;

        public GardenMetricsController(GitGardensDBContext context)
        {
            _context = context;
        }

        // GET: GardenMetrics/Create
        public IActionResult Create(int gardenId)
        {
            var model = new GardenMetrics { GardenId = gardenId };
            return View(model);
        }

        // POST: GardenMetrics/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GardenMetrics metrics)
        {
            if (!ModelState.IsValid)
                return View(metrics);

            metrics.RecordedAt = System.DateTime.UtcNow;
            _context.GardenMetrics.Add(metrics);
            await _context.SaveChangesAsync();

            // Generate tips and pass them to TempData for the next request
            var tips = MetricTipsService.GenerateTips(metrics);
            TempData["MetricTips"] = string.Join("||", tips);

            return RedirectToAction("Details", "Gardens", new { id = metrics.GardenId });
        }
>>>>>>> Stashed changes
    }
}
