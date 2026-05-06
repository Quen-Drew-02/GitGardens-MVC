using GitGardens.Interface;
using GitGardens.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

/*
        Title: Disclosure of AI Usage in my Assessment.
        • Section: DashboardController.
        • AI Tool: Gemini
        • Purpose/intention : Design and Implementation of Dashboard functionalities, dashboard viewmodel and dashbard index.
        • Date(s) 05/05/2026.
        • https://gemini.google.com/share/75f6e6bedd8e
        */


namespace GitGardens.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly IGardenService _gardenService;

        public DashboardController(IGardenService gardenService)
        {
            _gardenService = gardenService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userIDClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIDClaim == null) return Unauthorized();

            int userID = int.Parse(userIDClaim.Value);

            // Fetch all gardens for the user
            var allGardens = await _gardenService.GetUserGardensAsync(userID);

            var dashboardModel = new DashboardViewModel
            {
                TotalGardens = allGardens.Count(),
                NeedsAttentionCount = 0,
                AverageHealth = 0
            };

            var recentGardens = new List<RecentGardenSummary>();
            int totalHealth = 0;
            int gardensWithMetricsCount = 0;

            foreach (var garden in allGardens)
            {
                var latestMetric = garden.GardenMetrics?.OrderByDescending(m => m.RecordedAt).FirstOrDefault();
                int currentHealth = latestMetric != null ? _gardenService.CalculateHealthScore(latestMetric) : 0;

                if (latestMetric != null)
                {
                    totalHealth += currentHealth;
                    gardensWithMetricsCount++;
                }

                if (currentHealth < 50 && latestMetric != null)
                {
                    dashboardModel.NeedsAttentionCount++;
                }

                recentGardens.Add(new RecentGardenSummary
                {
                    GardenID = garden.GardenId,
                    GardenName = garden.GardenName,
                    HealthScore = currentHealth,
                    LastRecordedAt = latestMetric?.RecordedAt
                });
            }

            // Calculate overall average health
            if (gardensWithMetricsCount > 0)
            {
                dashboardModel.AverageHealth = totalHealth / gardensWithMetricsCount;
            }

            // Order by the most recently recorded metrics, take top 5
            dashboardModel.RecentGardens = recentGardens
                .OrderByDescending(g => g.LastRecordedAt ?? DateTime.MinValue)
                .Take(5)
                .ToList();

            return View(dashboardModel);
        }
    }
}
